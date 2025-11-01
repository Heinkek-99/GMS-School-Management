# Script de création et application de migration
param(
    [string]$MigrationName = "InitialCreate"
)

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  GMS - Création Migration EF Core     " -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Vérifier que nous sommes dans le bon dossier
if (-not (Test-Path "GMS.sln")) {
    Write-Host "❌ Erreur: Vous devez être dans le dossier racine du projet (contenant GMS.sln)" -ForegroundColor Red
    exit 1
}

Write-Host "📦 Migration : $MigrationName" -ForegroundColor Yellow
Write-Host ""

# Étape 1 : Supprimer les anciennes migrations (si c'est la première fois)
if ($MigrationName -eq "InitialCreate") {
    Write-Host "🗑️  Suppression des anciennes migrations..." -ForegroundColor Yellow
    
    $migrationsFolder = "src/GMS.Infrastructure/Data/Migrations"
    if (Test-Path $migrationsFolder) {
        Remove-Item -Path $migrationsFolder -Recurse -Force
        Write-Host "  ✅ Anciennes migrations supprimées" -ForegroundColor Green
    }
}

Write-Host ""

# Étape 2 : Créer la migration
Write-Host "📝 Création de la migration..." -ForegroundColor Yellow

try {
    dotnet ef migrations add $MigrationName `
        --project src/GMS.Infrastructure/GMS.Infrastructure.csproj `
        --startup-project src/GMS.Desktop/GMS.Desktop.csproj `
        --output-dir Data/Migrations `
        --verbose
    
    Write-Host ""
    Write-Host "  ✅ Migration créée avec succès!" -ForegroundColor Green
} catch {
    Write-Host ""
    Write-Host "  ❌ Erreur lors de la création de la migration" -ForegroundColor Red
    Write-Host "  $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

Write-Host ""

# Étape 3 : Appliquer la migration
Write-Host "🔄 Application de la migration à la base de données..." -ForegroundColor Yellow

try {
    dotnet ef database update `
        --project src/GMS.Infrastructure/GMS.Infrastructure.csproj `
        --startup-project src/GMS.Desktop/GMS.Desktop.csproj `
        --verbose
    
    Write-Host ""
    Write-Host "  ✅ Migration appliquée avec succès!" -ForegroundColor Green
} catch {
    Write-Host ""
    Write-Host "  ❌ Erreur lors de l'application de la migration" -ForegroundColor Red
    Write-Host "  $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

Write-Host ""

# Étape 4 : Vérifier la base de données
Write-Host "✅ Vérification de la base de données..." -ForegroundColor Yellow

try {
    # Connexion à SQL Server pour vérifier
    sqlcmd -S "(localdb)\mssqllocaldb" -Q "SELECT name FROM sys.databases WHERE name = 'GmsDb'" 2>$null
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "  ✅ Base de données GmsDb créée et accessible" -ForegroundColor Green
    }
} catch {
    Write-Host "  ⚠️  Impossible de vérifier la base (sqlcmd non trouvé)" -ForegroundColor Yellow
}

Write-Host ""

# Résumé
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  ✅ MIGRATION TERMINÉE!                " -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "📊 Tables créées:" -ForegroundColor Yellow
Write-Host "  - Utilisateurs"
Write-Host "  - Familles"
Write-Host "  - Eleves"
Write-Host "  - Classes"
Write-Host "  - AnneesScolaires"
Write-Host "  - Periodes"
Write-Host "  - TypesFrais"
Write-Host "  - Frais"
Write-Host "  - Paiements"
Write-Host "  - VentilationsPaiements"
Write-Host "  - Matieres"
Write-Host "  - Notes"
Write-Host "  - EmploisDuTemps"
Write-Host "  - AuditLogs"
Write-Host ""
Write-Host "🔗 Chaîne de connexion:" -ForegroundColor Yellow
Write-Host "  Server=(localdb)\mssqllocaldb;Database=GmsDb" -ForegroundColor Cyan
Write-Host ""
Write-Host "🎯 PROCHAINES ÉTAPES:" -ForegroundColor Yellow
Write-Host "  1. Insérer les données de seed (utilisateurs, classes, etc.)"
Write-Host "  2. Tester la connexion avec Azure Data Studio/SSMS"
Write-Host "  3. Développer le layer Application (CQRS)"
Write-Host ""