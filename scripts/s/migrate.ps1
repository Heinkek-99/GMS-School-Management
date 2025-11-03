# GMS - Script de migration EF Core
Write-Host "==========================================" -ForegroundColor Cyan
Write-Host "🚀 GMS - Première Migration EF Core" -ForegroundColor Cyan
Write-Host "==========================================" -ForegroundColor Cyan

# Variables
$InfraProject = "src/GMS.Infrastructure/GMS.Infrastructure.csproj"
$StartupProject = "src/GMS.Desktop/GMS.Desktop.csproj"

# 1. Vérifier que les projets existent
Write-Host "`n📁 Vérification des projets..." -ForegroundColor Yellow
if (-not (Test-Path $InfraProject)) {
    Write-Host "❌ Projet Infrastructure introuvable: $InfraProject" -ForegroundColor Red
    exit 1
}
if (-not (Test-Path $StartupProject)) {
    Write-Host "❌ Projet Startup introuvable: $StartupProject" -ForegroundColor Red
    exit 1
}
Write-Host "✅ Projets trouvés" -ForegroundColor Green

# 2. Vérifier l'installation de dotnet-ef
Write-Host "`n🔧 Vérification de dotnet-ef..." -ForegroundColor Yellow
$efVersion = dotnet ef --version 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-Host "⚠️  dotnet-ef non installé. Installation en cours..." -ForegroundColor Yellow
    dotnet tool install --global dotnet-ef
} else {
    Write-Host "✅ dotnet-ef version: $efVersion" -ForegroundColor Green
}

# 3. Supprimer les anciennes migrations (optionnel)
Write-Host "`n🗑️  Suppression des anciennes migrations..." -ForegroundColor Yellow
$migrationsPath = "src/GMS.Infrastructure/Persistence/Migrations"
if (Test-Path $migrationsPath) {
    Remove-Item -Path $migrationsPath -Recurse -Force
    Write-Host "✅ Anciennes migrations supprimées" -ForegroundColor Green
}

# 4. Créer la migration InitialCreate
Write-Host "`n📝 Création de la migration InitialCreate..." -ForegroundColor Yellow
dotnet ef migrations add InitialCreate `
    --project $InfraProject `
    --startup-project $StartupProject `
    --output-dir Data/Migrations `
    --context GmsDbContext `
    --verbose

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Erreur lors de la création de la migration" -ForegroundColor Red
    exit 1
}
Write-Host "✅ Migration créée avec succès" -ForegroundColor Green

# 5. Démarrer SQL Server avec Docker
Write-Host "`n🐳 Démarrage de SQL Server (Docker)..." -ForegroundColor Yellow
docker-compose up -d sqlserver

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Erreur Docker. Assurez-vous que Docker est démarré" -ForegroundColor Red
    exit 1
}

# 6. Attendre que SQL Server soit prêt
Write-Host "⏳ Attente du démarrage de SQL Server (40 secondes)..." -ForegroundColor Gray
Start-Sleep -Seconds 40

# 7. Tester la connexion SQL Server
Write-Host "`n🔌 Test de connexion SQL Server..." -ForegroundColor Yellow
$testConnection = docker exec gms-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost,1433 -U sa -P "Azerty12" -Q "SELECT 1" 2>&1
if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ Connexion SQL Server OK" -ForegroundColor Green
} else {
    Write-Host "❌ Impossible de se connecter à SQL Server" -ForegroundColor Red
    Write-Host $testConnection -ForegroundColor Red
    exit 1
}

# 8. Appliquer la migration
Write-Host "`n🎯 Application de la migration sur la base de données..." -ForegroundColor Yellow
dotnet ef database update `
    --project $InfraProject `
    --startup-project $StartupProject `
    --context GmsDbContext `
    --verbose

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Erreur lors de l'application de la migration" -ForegroundColor Red
    exit 1
}
Write-Host "✅ Migration appliquée avec succès" -ForegroundColor Green

# 9. Vérifier les tables créées
Write-Host "`n📊 Vérification des tables créées..." -ForegroundColor Yellow
$tables = docker exec gms-sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "Azerty12" -d GmsDb -Q "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' ORDER BY TABLE_NAME" -h -1

Write-Host "`n✅ Tables créées:" -ForegroundColor Green
Write-Host $tables -ForegroundColor White

# 10. Résumé final
Write-Host "`n==========================================" -ForegroundColor Cyan
Write-Host "🎉 MIGRATION RÉUSSIE !" -ForegroundColor Green
Write-Host "==========================================" -ForegroundColor Cyan

Write-Host "`n📋 Prochaines étapes:" -ForegroundColor Yellow
Write-Host "  1. Vérifier les tables dans SSMS" -ForegroundColor White
Write-Host "  2. Exécuter le script de seed data" -ForegroundColor White
Write-Host "  3. Tester les queries CQRS" -ForegroundColor White
Write-Host "  4. Lancer l'application Desktop" -ForegroundColor White

Write-Host "`n🔍 Commandes utiles:" -ForegroundColor Yellow
Write-Host "  - Logs SQL Server: docker logs gms-sqlserver" -ForegroundColor Gray
Write-Host "  - Arrêter Docker: docker-compose down" -ForegroundColor Gray
Write-Host "  - Relancer migration: dotnet ef database update" -ForegroundColor Gray

Write-Host "`n✨ Migration terminée avec succès !" -ForegroundColor Green