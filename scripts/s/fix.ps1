# GMS - Script de migration corrigé
Write-Host "==========================================" -ForegroundColor Cyan
Write-Host "🚀 GMS - Migration avec Docker SQL Server" -ForegroundColor Cyan
Write-Host "==========================================" -ForegroundColor Cyan

# Variables
$InfraProject = "src/GMS.Infrastructure/GMS.Infrastructure.csproj"
$StartupProject = "src/GMS.Desktop/GMS.Desktop.csproj"

# 1. Vérifier les projets
Write-Host "`n📁 Vérification des projets..." -ForegroundColor Yellow
if (-not (Test-Path $InfraProject)) {
    Write-Host "❌ Projet Infrastructure introuvable" -ForegroundColor Red
    exit 1
}
Write-Host "✅ Projets trouvés" -ForegroundColor Green

# 2. Clean + Restore + Build
Write-Host "`n🔨 Build du projet..." -ForegroundColor Yellow
dotnet clean
dotnet restore
dotnet build

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Erreur de build" -ForegroundColor Red
    exit 1
}
Write-Host "✅ Build réussi" -ForegroundColor Green

# 3. Arrêter l'ancien conteneur (si existe)
Write-Host "`n🛑 Arrêt de l'ancien conteneur SQL Server..." -ForegroundColor Yellow
docker-compose down -v

# 4. Démarrer SQL Server
Write-Host "`n🐳 Démarrage de SQL Server..." -ForegroundColor Yellow
docker-compose up -d sqlserver

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Erreur Docker" -ForegroundColor Red
    exit 1
}

# 5. Attendre que SQL Server soit prêt
Write-Host "`n⏳ Attente du démarrage de SQL Server (60 secondes)..." -ForegroundColor Gray
Start-Sleep -Seconds 60

# 6. Tester la connexion avec sqlcmd18
Write-Host "`n🔌 Test de connexion SQL Server..." -ForegroundColor Yellow
$maxRetries = 10
$retryCount = 0
$connected = $false

while ($retryCount -lt $maxRetries -and -not $connected) {
    try {
        # Utiliser sqlcmd18 (nouvelle version)
        $testResult = docker exec gms-sqlserver /opt/mssql-tools18/bin/sqlcmd `
            -S localhost -U sa -P "Azerty12" -Q "SELECT 1" -C 2>&1
        
        if ($LASTEXITCODE -eq 0) {
            $connected = $true
            Write-Host "✅ Connexion SQL Server OK" -ForegroundColor Green
        } else {
            throw "Connexion échouée"
        }
    }
    catch {
        $retryCount++
        Write-Host "⏳ Tentative $retryCount/$maxRetries..." -ForegroundColor Yellow
        Start-Sleep -Seconds 5
    }
}

if (-not $connected) {
    Write-Host "❌ Impossible de se connecter à SQL Server après $maxRetries tentatives" -ForegroundColor Red
    Write-Host "`n💡 Vérifiez les logs:" -ForegroundColor Yellow
    Write-Host "   docker logs gms-sqlserver" -ForegroundColor White
    exit 1
}

# 7. Créer la migration (si pas déjà créée)
Write-Host "`n📝 Vérification de la migration..." -ForegroundColor Yellow
$migrationsPath = "src/GMS.Infrastructure/Data/Migrations"

if (-not (Test-Path $migrationsPath)) {
    Write-Host "Création de la migration InitialCreate..." -ForegroundColor Yellow
    dotnet ef migrations add InitialCreate `
        --project $InfraProject `
        --startup-project $StartupProject `
        --output-dir Data/Migrations `
        --context GmsDbContext

    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ Erreur lors de la création de la migration" -ForegroundColor Red
        exit 1
    }
    Write-Host "✅ Migration créée" -ForegroundColor Green
} else {
    Write-Host "✅ Migration déjà existante" -ForegroundColor Green
}

# 8. Appliquer la migration
Write-Host "`n🎯 Application de la migration..." -ForegroundColor Yellow
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
Write-Host "`n📊 Vérification des tables..." -ForegroundColor Yellow
$tables = docker exec gms-sqlserver /opt/mssql-tools18/bin/sqlcmd `
    -S localhost -U sa -P "Azerty12" -d GMSDb `
    -Q "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' ORDER BY TABLE_NAME" `
    -C -h -1 2>&1

if ($LASTEXITCODE -eq 0) {
    Write-Host "`n✅ Tables créées:" -ForegroundColor Green
    Write-Host $tables -ForegroundColor White
}

# 10. Résumé
Write-Host "`n==========================================" -ForegroundColor Cyan
Write-Host "🎉 CONFIGURATION TERMINÉE !" -ForegroundColor Green
Write-Host "==========================================" -ForegroundColor Cyan

Write-Host "`n📋 Prochaines étapes:" -ForegroundColor Yellow
Write-Host "  1. Lancer l'application: dotnet run --project src/GMS.Desktop" -ForegroundColor White
Write-Host "  2. Se connecter avec: admin / Admin@123" -ForegroundColor White

Write-Host "`n🔍 Commandes utiles:" -ForegroundColor Yellow
Write-Host "  - Logs SQL Server: docker logs gms-sqlserver" -ForegroundColor Gray
Write-Host "  - Arrêter Docker: docker-compose down" -ForegroundColor Gray
Write-Host "  - Supprimer données: docker-compose down -v" -ForegroundColor Gray
Write-Host "  - SSMS: Server=localhost,1433 | User=sa | Password=Azerty12" -ForegroundColor Gray

Write-Host "`n✨ Configuration terminée avec succès !" -ForegroundColor Green