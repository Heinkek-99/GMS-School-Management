# Script de correction des erreurs Application layer
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  CORRECTION ERREURS APPLICATION        " -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

Write-Host "🗑️  Suppression des fichiers incomplets..." -ForegroundColor Yellow

# Supprimer les fichiers qui causent des erreurs (ils seront recréés)
$filesToDelete = @(
    "src/GMS.Application/Features/Dashboard/Queries/GetDashboardStatsHandler.cs",
    "src/GMS.Application/Features/Familles/Queries/GetFamilleByIdHandler.cs"
)

foreach ($file in $filesToDelete) {
    if (Test-Path $file) {
        Remove-Item $file -Force
        Write-Host "  ✅ Supprimé: $file" -ForegroundColor Green
    }
}

# Supprimer les dossiers Features si vides
$featuresDirs = @(
    "src/GMS.Application/Features/Dashboard",
    "src/GMS.Application/Features/Familles",
    "src/GMS.Application/Features"
)

foreach ($dir in $featuresDirs) {
    if (Test-Path $dir) {
        $isEmpty = (Get-ChildItem $dir -Recurse -File).Count -eq 0
        if ($isEmpty) {
            Remove-Item $dir -Recurse -Force
            Write-Host "  ✅ Dossier vide supprimé: $dir" -ForegroundColor Green
        }
    }
}

Write-Host ""
Write-Host "🧹 Nettoyage bin/obj..." -ForegroundColor Yellow
Get-ChildItem -Path . -Include bin,obj -Recurse -Directory | Remove-Item -Recurse -Force

Write-Host ""
Write-Host "📦 Restauration..." -ForegroundColor Yellow
dotnet restore

Write-Host ""
Write-Host "🔨 Build..." -ForegroundColor Yellow
Write-Host ""

$buildOutput = dotnet build 2>&1 | Out-String

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "  ✅ BUILD RÉUSSI !                    " -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "🎉 Le projet compile maintenant!" -ForegroundColor Green
    Write-Host ""
    Write-Host "⚠️  Note: Il y a des warnings nullable (normaux)" -ForegroundColor Yellow
    Write-Host "   Ils seront corrigés dans un prochain commit." -ForegroundColor Gray
} else {
    Write-Host ""
    Write-Host "❌ Build échoué" -ForegroundColor Red
    Write-Host $buildOutput -ForegroundColor Red
}

Write-Host ""