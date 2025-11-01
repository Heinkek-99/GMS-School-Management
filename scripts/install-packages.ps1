# Script d'installation des packages NuGet
Write-Host "📦 Installation des packages NuGet..." -ForegroundColor Cyan
Write-Host ""

# GMS.Domain (pas de packages externes)
Write-Host "📘 GMS.Domain - Aucun package requis" -ForegroundColor Gray

# GMS.Application
Write-Host "📗 GMS.Application..." -ForegroundColor Yellow
dotnet add src/GMS.Application/GMS.Application.csproj package MediatR --version 12.2.0
dotnet add src/GMS.Application/GMS.Application.csproj package FluentValidation --version 11.9.0
dotnet add src/GMS.Application/GMS.Application.csproj package FluentValidation.DependencyInjectionExtensions --version 11.9.0

# GMS.Infrastructure
Write-Host "📙 GMS.Infrastructure..." -ForegroundColor Yellow
dotnet add src/GMS.Infrastructure/GMS.Infrastructure.csproj package Microsoft.EntityFrameworkCore --version 8.0.1
dotnet add src/GMS.Infrastructure/GMS.Infrastructure.csproj package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.1
dotnet add src/GMS.Infrastructure/GMS.Infrastructure.csproj package Microsoft.EntityFrameworkCore.Tools --version 8.0.1
dotnet add src/GMS.Infrastructure/GMS.Infrastructure.csproj package Microsoft.EntityFrameworkCore.Design --version 8.0.1
dotnet add src/GMS.Infrastructure/GMS.Infrastructure.csproj package BCrypt.Net-Next --version 4.0.3
dotnet add src/GMS.Infrastructure/GMS.Infrastructure.csproj package Serilog --version 3.1.1
dotnet add src/GMS.Infrastructure/GMS.Infrastructure.csproj package Serilog.Sinks.File --version 5.0.0

# GMS.Desktop
Write-Host "📕 GMS.Desktop..." -ForegroundColor Yellow
dotnet add src/GMS.Desktop/GMS.Desktop.csproj package Microsoft.Extensions.DependencyInjection --version 8.0.0
dotnet add src/GMS.Desktop/GMS.Desktop.csproj package Microsoft.Extensions.Configuration --version 8.0.0
dotnet add src/GMS.Desktop/GMS.Desktop.csproj package Microsoft.Extensions.Configuration.Json --version 8.0.0
dotnet add src/GMS.Desktop/GMS.Desktop.csproj package Serilog.Sinks.Console --version 5.0.1

# GMS.Tests
Write-Host "🧪 GMS.Tests..." -ForegroundColor Yellow
dotnet add src/GMS.Tests/GMS.Tests.csproj package xunit --version 2.6.4
dotnet add src/GMS.Tests/GMS.Tests.csproj package xunit.runner.visualstudio --version 2.5.6
dotnet add src/GMS.Tests/GMS.Tests.csproj package FluentAssertions --version 6.12.0
dotnet add src/GMS.Tests/GMS.Tests.csproj package Moq --version 4.20.70
dotnet add src/GMS.Tests/GMS.Tests.csproj package Microsoft.NET.Test.Sdk --version 17.8.0

Write-Host ""
Write-Host "✅ Tous les packages installés!" -ForegroundColor Green
Write-Host ""
Write-Host "🔨 Restauration et build..." -ForegroundColor Yellow
dotnet restore
dotnet build

Write-Host ""
Write-Host "✅ Setup des packages terminé!" -ForegroundColor Green