# Script de migration base de données
param(
    [string]$MigrationName = "NewMigration"
)

Write-Host "🗄️ Création de la migration : $MigrationName" -ForegroundColor Yellow

# Créer la migration
dotnet ef migrations add $MigrationName `
    --project src/GMS.Infrastructure `
    --startup-project src/GMS.Desktop `
    --output-dir Data/Migrations

# Appliquer la migration
Write-Host "📝 Application de la migration..." -ForegroundColor Yellow
dotnet ef database update `
    --project src/GMS.Infrastructure `
    --startup-project src/GMS.Desktop

Write-Host "✅ Migration appliquée avec succès!" -ForegroundColor Green