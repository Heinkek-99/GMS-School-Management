# Script de configuration environnement développement
Write-Host "🚀 Configuration environnement GMS..." -ForegroundColor Green

# Vérifier les prérequis
Write-Host "Vérification des outils installés..." -ForegroundColor Yellow

# Git
if (Get-Command git -ErrorAction SilentlyContinue) {
    Write-Host "✅ Git installé : $(git --version)" -ForegroundColor Green
} else {
    Write-Host "❌ Git non installé" -ForegroundColor Red
    exit 1
}

# .NET
if (Get-Command dotnet -ErrorAction SilentlyContinue) {
    Write-Host "✅ .NET installé : $(dotnet --version)" -ForegroundColor Green
} else {
    Write-Host "❌ .NET SDK non installé" -ForegroundColor Red
    exit 1
}

# Docker
if (Get-Command docker -ErrorAction SilentlyContinue) {
    Write-Host "✅ Docker installé : $(docker --version)" -ForegroundColor Green
} else {
    Write-Host "❌ Docker non installé" -ForegroundColor Red
    exit 1
}

# Restaurer les packages NuGet
Write-Host "📦 Restauration des packages NuGet..." -ForegroundColor Yellow
dotnet restore

# Créer la base de données
Write-Host "🗄️ Création de la base de données..." -ForegroundColor Yellow
dotnet ef database update --project src/GMS.Infrastructure --startup-project src/GMS.Desktop

# Seed des données
Write-Host "🌱 Insertion des données de test..." -ForegroundColor Yellow
# Le seed se fait automatiquement au démarrage

# Build du projet
Write-Host "🔨 Build du projet..." -ForegroundColor Yellow
dotnet build

Write-Host "✅ Configuration terminée avec succès!" -ForegroundColor Green
Write-Host "Lancez l'application avec : dotnet run --project src/GMS.Desktop" -ForegroundColor Cyan