# restore-and-build.ps1
param(
    [switch]$Restore = $false,
    [switch]$Build = $false
)

# Fonction de gestion des erreurs
function Handle-Error {
    param($Message)
    Write-Host $Message -ForegroundColor Red
    exit 1
}

# Restauration des packages
if ($Restore -or $Build) {
    Write-Host "🔄 Restauration des packages..." -ForegroundColor Cyan
    dotnet restore GMS.sln
    if ($LASTEXITCODE -ne 0) {
        Handle-Error "Erreur lors de la restauration des packages"
    }
}

# Build du projet
if ($Build) {
    Write-Host "🏗️ Construction du projet..." -ForegroundColor Yellow
    dotnet build GMS.sln --no-restore
    if ($LASTEXITCODE -ne 0) {
        Handle-Error "Erreur lors de la compilation du projet"
    }
}

Write-Host "✅ Opération terminée avec succès!" -ForegroundColor Green
