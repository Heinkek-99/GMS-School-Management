# check-constants.ps1
$constantsPath = ".\src\GMS.Shared\Constants\ModePaiement.cs"

# Vérifier l'existence du fichier
if (-Not (Test-Path $constantsPath)) {
    Write-Host "Fichier non trouvé : $constantsPath" -ForegroundColor Red
    exit 1
}

# Lire le contenu du fichier
$content = Get-Content $constantsPath

# Vérifier le contenu
$hasErrors = $false

if ($content -notmatch 'public static class ModePaiement') {
    Write-Host "Erreur : Classe ModePaiement manquante" -ForegroundColor Red
    $hasErrors = $true
}

if ($content -notmatch 'public const string Espece = "Espece"') {
    Write-Host "Erreur : Constante Espece incorrecte" -ForegroundColor Red
    $hasErrors = $true
}

if ($hasErrors) {
    exit 1
}

Write-Host "Vérification des constantes réussie !" -ForegroundColor Green
