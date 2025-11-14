# ============================================
# GMS - Script de Migration Complet avec Docker
# ============================================

Write-Host "==========================================" -ForegroundColor Cyan
Write-Host "    🚀 GMS - Migration Complète EF Core" -ForegroundColor Cyan
Write-Host "==========================================" -ForegroundColor Cyan

# Variables
$InfraProject = "src/GMS.Infrastructure/GMS.Infrastructure.csproj"
$DesktopProject = "src/GMS.Desktop/GMS.Desktop.csproj"
$MigrationName = "InitialCreate"
$DbName = "GmsDb"
$SqlPassword = "Azerty12"
$SqlPort = 11433

# ============================================
# ÉTAPE 1 : Vérifications préalables
# ============================================
Write-Host "`n📋 ÉTAPE 1/10 : Vérifications préalables..." -ForegroundColor Yellow

# Vérifier projets
if (-not (Test-Path $InfraProject)) {
    Write-Host "❌ Projet Infrastructure introuvable: $InfraProject" -ForegroundColor Red
    exit 1
}
if (-not (Test-Path $DesktopProject)) {
    Write-Host "❌ Projet Desktop introuvable: $DesktopProject" -ForegroundColor Red
    exit 1
}
Write-Host "✅ Projets trouvés" -ForegroundColor Green

# Vérifier Docker
Write-Host "`n🐳 Vérification de Docker..." -ForegroundColor Yellow
$dockerVersion = docker --version 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Docker n'est pas installé ou n'est pas démarré" -ForegroundColor Red
    Write-Host "   Installez Docker Desktop depuis: https://www.docker.com/products/docker-desktop" -ForegroundColor Gray
    exit 1
}
Write-Host "✅ Docker version: $dockerVersion" -ForegroundColor Green

# Vérifier dotnet-ef
Write-Host "`n🔧 Vérification de dotnet-ef..." -ForegroundColor Yellow
$efVersion = dotnet ef --version 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Host "⚠️  dotnet-ef non installé. Installation en cours..." -ForegroundColor Yellow
    dotnet tool install --global dotnet-ef
    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ Échec de l'installation de dotnet-ef" -ForegroundColor Red
        exit 1
    }
}
Write-Host "✅ dotnet-ef version: $efVersion" -ForegroundColor Green

# ============================================
# ÉTAPE 2 : Supprimer anciennes migrations
# ============================================
Write-Host "`n🗑️  ÉTAPE 2/10 : Suppression des anciennes migrations..." -ForegroundColor Yellow

$migrationsPath = "src/GMS.Infrastructure/Data/Migrations"
if (Test-Path $migrationsPath) {
    Remove-Item -Path $migrationsPath -Recurse -Force -ErrorAction SilentlyContinue
    Write-Host "✅ Anciennes migrations supprimées" -ForegroundColor Green
} else {
    Write-Host "ℹ️  Aucune migration existante" -ForegroundColor Gray
}

# ============================================
# ÉTAPE 3 : Arrêter et nettoyer Docker
# ============================================
Write-Host "`n🧹 ÉTAPE 3/10 : Nettoyage Docker..." -ForegroundColor Yellow
# Arrêter docker-compose
Write-Host "Arrêt docker-compose..." -ForegroundColor Gray
docker-compose down -v 2>$null

# Arrêter les conteneurs existants
Write-Host "Arrêt des conteneurs GMS existants..." -ForegroundColor Gray
docker stop gms-sqlserver 2>$null
docker rm gms-sqlserver 2>$null

# Supprimer le volume (données)
Write-Host "Suppression des volumes..." -ForegroundColor Gray
docker volume rm gms-school-management_sqlserver-data 2>$null
docker volume rm gms-sql-data 2>$null

Write-Host "✅ Nettoyage Docker terminé" -ForegroundColor Green

# ============================================
# ÉTAPE 4 : Démarrer SQL Server avec Docker
# ============================================

Write-Host "`n🐳 ÉTAPE 4/10 : Démarrage de SQL Server..." -ForegroundColor Yellow

docker-compose up -d sqlserver

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Erreur lors du démarrage de docker-compose" -ForegroundColor Red
    Write-Host "Vérifiez que le fichier docker-compose.yml existe" -ForegroundColor Yellow
    exit 1
}

Write-Host "✅ Docker Compose démarré" -ForegroundColor Green

# Attendre que SQL Server soit prêt
Write-Host "⏳ Attente du démarrage de SQL Server (30 secondes)..." -ForegroundColor Gray
Start-Sleep -Seconds 30

# ============================================
# ÉTAPE 5 : Tester la connexion SQL Server
# ============================================
Write-Host "`n🔌 ÉTAPE 5/10 : Test de connexion SQL Server..." -ForegroundColor Yellow

$maxRetries = 12
$retryCount = 0
$connected = $false

while (-not $connected -and $retryCount -lt $maxRetries) {
    try{
         Write-Host "⏳ Tentative $($retryCount + 1)/$maxRetries..." -ForegroundColor Gray
        
        # ✅ CORRECTION : Utiliser /opt/mssql-tools18/bin/sqlcmd (avec 18)
        # ET ajouter -C pour accepter le certificat auto-signé

        $testConnection = docker exec gms-sqlserver /opt/mssql-tools18/bin/sqlcmd `
            -S localhost -U sa -P "$SqlPassword" -C -Q "SELECT 1" 2>&1
     
        if ($LASTEXITCODE -eq 0) {
            $connected = $true
            Write-Host "✅ SQL Server est prêt" -ForegroundColor Green
        } else {
            throw "Connexion échouée"
        }
    }
    catch {
        $retryCount++
        # Write-Host "⏳ Tentative $retryCount/$maxRetries..." -ForegroundColor Yellow
        if ($retryCount -lt $maxRetries) {
            Start-Sleep -Seconds 10
        }
    }   
}

if (-not $connected) {
    Write-Host "❌ Impossible de se connecter à SQL Server après $maxRetries tentatives" -ForegroundColor Red
    Write-Host "`n📋 Logs SQL Server (dernières 100 lignes):" -ForegroundColor Yellow
    docker logs gms-sqlserver --tail 100
    Write-Host "`n💡 Solutions possibles:" -ForegroundColor Yellow
    Write-Host "  1. Attendez 2 minutes et relancez le script" -ForegroundColor White
    Write-Host "  2. Vérifiez que Docker a assez de mémoire (min 2GB)" -ForegroundColor White
    Write-Host "  3. Redémarrez Docker Desktop" -ForegroundColor White
    exit 1
}

# ============================================
# ÉTAPE 6 : Build de la solution
# ============================================
Write-Host "`n🔨 ÉTAPE 6/10 : Build de la solution..." -ForegroundColor Yellow

dotnet build $InfraProject --configuration Release -v normal

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Échec du build" -ForegroundColor Red
    exit 1
}
Write-Host "✅ Build réussi" -ForegroundColor Green

# ============================================
# ÉTAPE 7 : Créer la migration
# ============================================
Write-Host "`n📝 ÉTAPE 6/10 : Création de la migration..." -ForegroundColor Yellow

dotnet ef migrations add $MigrationName `
    --project $InfraProject `
    --startup-project $DesktopProject `
    --output-dir Data/Migrations `
    --context GmsDbContext `
    --verbose
    # 2>&1 | Out-Null

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Échec de la création de la migration" -ForegroundColor Red
    exit 1
}
Write-Host "✅ Migration '$MigrationName' créée avec succès" -ForegroundColor Green

# ============================================
# ÉTAPE 8 : Créer la base de données
# ============================================
Write-Host "`n💾 ÉTAPE 8/10 : Création de la base de données..." -ForegroundColor Yellow

$createDbSql = "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '$DbName') CREATE DATABASE [$DbName]"

docker exec gms-sqlserver /opt/mssql-tools18/bin/sqlcmd `
    -S localhost -U sa -P "$SqlPassword" -C -Q "$createDbSql" 2>&1 | Out-Null

if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ Base de données '$DbName' créée" -ForegroundColor Green
} else {
    Write-Host "❌ Échec de la création de la base de données" -ForegroundColor Red
    exit 1
}

# ============================================
# ÉTAPE 9 : Appliquer la migration
# ============================================
Write-Host "`n🎯 ÉTAPE 9/10 : Application de la migration..." -ForegroundColor Yellow

dotnet ef database update `
    --project $InfraProject `
    --startup-project $DesktopProject `
    --context GmsDbContext `
    --verbose


if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Échec de l'application de la migration" -ForegroundColor Red
    Write-Host "`nRelancer avec --verbose pour voir les détails:" -ForegroundColor Yellow
    Write-Host "dotnet ef database update --project $InfraProject --startup-project $DesktopProject --verbose" -ForegroundColor Gray
    Write-Host "Logs SQL Server:" -ForegroundColor Yellow
    docker logs gms-sqlserver --tail 50
    exit 1
}
Write-Host "✅ Migration appliquée avec succès" -ForegroundColor Green

# ============================================
# ÉTAPE 10 : Vérification des données
# ============================================
Write-Host "`n📊 ÉTAPE 10/10 : Vérification des données..." -ForegroundColor Yellow

# Compter les tables
$tablesQuery = "USE [$DbName]; SELECT COUNT(*) as TableCount FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'"
$tableCount = docker exec gms-sqlserver /opt/mssql-tools18/bin/sqlcmd `
    -S localhost -U sa -P "$SqlPassword" -C -Q "$tablesQuery" -h -1 -W 2>&1

if ($LASTEXITCODE -eq 0) {
    Write-Host "📋 Nombre de tables créées: $($tableCount.Trim())" -ForegroundColor Cyan
}

# Vérifier les données seed
Write-Host "`n📈 Données seed:" -ForegroundColor Cyan

$queries = @(
    @{ Name = "Écoles"; Query = "USE [$DbName]; SELECT COUNT(*) FROM Ecoles" },
    @{ Name = "Utilisateurs"; Query = "USE [$DbName]; SELECT COUNT(*) FROM Utilisateurs" },
    @{ Name = "Années Scolaires"; Query = "USE [$DbName]; SELECT COUNT(*) FROM AnneesScolaires" },
    @{ Name = "Périodes"; Query = "USE [$DbName]; SELECT COUNT(*) FROM Periodes" },
    @{ Name = "Classes"; Query = "USE [$DbName]; SELECT COUNT(*) FROM Classes" },
    @{ Name = "Types de Frais"; Query = "USE [$DbName]; SELECT COUNT(*) FROM TypesFrais" },
    @{ Name = "Familles"; Query = "USE [$DbName]; SELECT COUNT(*) FROM Familles" },
    @{ Name = "Élèves"; Query = "USE [$DbName]; SELECT COUNT(*) FROM Eleves" },
    @{ Name = "Frais"; Query = "USE [$DbName]; SELECT COUNT(*) FROM Frais" },
    @{ Name = "Matières"; Query = "USE [$DbName]; SELECT COUNT(*) FROM Matieres" }
)

Write-Host "`n📈 Données seed:" -ForegroundColor Cyan
foreach ($q in $queries) {
   
    try{

        $result = docker exec gms-sqlserver /opt/mssql-tools18/bin/sqlcmd `
            -S localhost -U sa -P "$SqlPassword" -C -Q "$($q.Query)" -h -1 -W 2>&1
            
        if ($LASTEXITCODE -eq 0 -and $result) {
            $count = $result.Trim()
            Write-Host "  ✅ $($q.Name): $count enregistrement(s)" -ForegroundColor Green
        } 
        else {
            Write-Host "  ⚠️  $($q.Name): Erreur de vérification" -ForegroundColor Yellow
        }
    }

    catch {
        Write-Host "  ⚠️  $($q.Name): Erreur" -ForegroundColor Yellow
    }
}

# ============================================
# ÉTAPE 10 : Afficher la ConnectionString
# ============================================
Write-Host "`n🔗 ÉTAPE 11/10 : Configuration finale..." -ForegroundColor Yellow

$connectionString = "Server=localhost,$SqlPort;Database=$DbName;User Id=sa;Password=$SqlPassword;MultipleActiveResultSets=True;TrustServerCertificate=True"

Write-Host "`n📝 ConnectionString à utiliser dans appsettings.json:" -ForegroundColor Cyan
Write-Host $connectionString -ForegroundColor White

# Créer/Mettre à jour appsettings.json
$appSettingsPath = "src/GMS.Desktop/appsettings.json"
$appSettingsContent = @"
{
  "ConnectionStrings": {
    "DefaultConnection": "$connectionString"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "Application": {
    "Name": "GMS - Gestion Scolaire",
    "Version": "1.0.0",
    "AnneeScolaireActive": "2024-2025"
  }
}
"@

Set-Content -Path $appSettingsPath -Value $appSettingsContent -Encoding UTF8
Write-Host "✅ appsettings.json mis à jour" -ForegroundColor Green

# ============================================
# RÉSUMÉ FINAL
# ============================================
Write-Host "`n==========================================" -ForegroundColor Cyan
Write-Host "🎉 MIGRATION RÉUSSIE !" -ForegroundColor Green
Write-Host "==========================================" -ForegroundColor Cyan

Write-Host "`n📊 Résumé:" -ForegroundColor Yellow
Write-Host "  • SQL Server: ✅ Démarré (Port $SqlPort)" -ForegroundColor White
Write-Host "  • Base de données: ✅ $DbName créée" -ForegroundColor White
Write-Host "  • Migration: ✅ $MigrationName appliquée" -ForegroundColor White
Write-Host "  • Seed data: ✅ Données initiales insérées" -ForegroundColor White

Write-Host "`n🔑 Identifiants SQL Server:" -ForegroundColor Yellow
Write-Host "  • Serveur: localhost,$SqlPort" -ForegroundColor White
Write-Host "  • User: sa" -ForegroundColor White
Write-Host "  • Password: $SqlPassword" -ForegroundColor White
Write-Host "  • Database: $DbName" -ForegroundColor White

Write-Host "`n👤 Comptes de test:" -ForegroundColor Yellow
Write-Host "  • Admin:      admin / Admin@123" -ForegroundColor White
Write-Host "  • Directeur:  directeur / Dir@123" -ForegroundColor White
Write-Host "  • Secrétaire: secretaire / Sec@123" -ForegroundColor White
Write-Host "  • Comptable:  comptable / Compta@123" -ForegroundColor White

Write-Host "`n📋 Prochaines étapes:" -ForegroundColor Yellow
Write-Host "  1. Lancer l'application: dotnet run --project $DesktopProject" -ForegroundColor White
Write-Host "  2. Tester avec Swagger: .\run-swagger-test.ps1" -ForegroundColor White
Write-Host "  3. Connecter avec SSMS (SQL Server Management Studio)" -ForegroundColor White

Write-Host "`n🔍 Commandes utiles:" -ForegroundColor Yellow
Write-Host "  • Logs SQL:        docker logs gms-sqlserver" -ForegroundColor Gray
Write-Host "  • Arrêter Docker:  docker stop gms-sqlserver" -ForegroundColor Gray
Write-Host "  • Démarrer Docker: docker start gms-sqlserver" -ForegroundColor Gray
Write-Host "  • Supprimer tout:  docker rm -f gms-sqlserver && docker volume rm gms-sql-data" -ForegroundColor Gray
Write-Host "  • Arrêter:         docker-compose down" -ForegroundColor Gray
Write-Host "  • Redémarrer:      docker-compose up -d" -ForegroundColor Gray
Write-Host "  • Tout supprimer:  docker-compose down -v" -ForegroundColor Gray

Write-Host "`n✨ Migration terminée avec succès !" -ForegroundColor Green
Write-Host "==========================================" -ForegroundColor Cyan