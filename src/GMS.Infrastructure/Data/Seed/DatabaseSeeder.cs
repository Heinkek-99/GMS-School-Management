// GMS.Infrastructure/Data/Seed/DatabaseSeeder.cs
using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GMS.Infrastructure.Data.Seed;

public class DatabaseSeeder
{
    private readonly GmsDbContext _context;

    public DatabaseSeeder(GmsDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        // Créer la base si elle n'existe pas
        await _context.Database.EnsureCreatedAsync();

        // Seed uniquement si la base est vide
        if (await _context.Utilisateurs.AnyAsync())
            return;

        await SeedUtilisateurs();
        await SeedAnneeScolaire();
        await SeedClasses();
        await SeedMatieres();
        await SeedTypesFrais();

        await _context.SaveChangesAsync();
    }

    private async Task SeedUtilisateurs()
    {
        var utilisateurs = new List<Utilisateur>
        {
            new Utilisateur
            {
                Id = Guid.NewGuid(),
                Nom = "ADMIN",
                Prenom = "System",
                Email = "admin@gms.local",
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = "Admin",
                EstActif = true,
                CreatedAt = DateTime.Now
            },
            new Utilisateur
            {
                Id = Guid.NewGuid(),
                Nom = "KOFFI",
                Prenom = "Jean",
                Email = "directeur@gms.local",
                Username = "directeur",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Dir@123"),
                Role = "Directeur",
                EstActif = true,
                CreatedAt = DateTime.Now
            },
            new Utilisateur
            {
                Id = Guid.NewGuid(),
                Nom = "TRAORE",
                Prenom = "Marie",
                Email = "secretaire@gms.local",
                Username = "secretaire",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Sec@123"),
                Role = "Secretaire",
                EstActif = true,
                CreatedAt = DateTime.Now
            },
            new Utilisateur
            {
                Id = Guid.NewGuid(),
                Nom = "DIALLO",
                Prenom = "Amadou",
                Email = "comptable@gms.local",
                Username = "comptable",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Compta@123"),
                Role = "Comptable",
                EstActif = true,
                CreatedAt = DateTime.Now
            }
        };

        await _context.Utilisateurs.AddRangeAsync(utilisateurs);
    }

    private async Task SeedAnneeScolaire()
    {
        var anneeActuelle = new AnneeScolaire
        {
            Id = Guid.NewGuid(),
            Libelle = "2024-2025",
            DateDebut = new DateTime(2024, 9, 1),
            DateFin = new DateTime(2025, 6, 30),
            EstActive = true,
            CreatedAt = DateTime.Now
        };

        await _context.AnneesScolaires.AddAsync(anneeActuelle);

        // Périodes (Trimestres)
        var periodes = new List<Periode>
        {
            new Periode
            {
                Id = Guid.NewGuid(),
                Libelle = "Trimestre 1",
                Numero = 1,
                DateDebut = new DateTime(2024, 9, 1),
                DateFin = new DateTime(2024, 12, 15),
                AnneeScolaireId = anneeActuelle.Id,
                CreatedAt = DateTime.Now
            },
            new Periode
            {
                Id = Guid.NewGuid(),
                Libelle = "Trimestre 2",
                Numero = 2,
                DateDebut = new DateTime(2025, 1, 5),
                DateFin = new DateTime(2025, 3, 31),
                AnneeScolaireId = anneeActuelle.Id,
                CreatedAt = DateTime.Now
            },
            new Periode
            {
                Id = Guid.NewGuid(),
                Libelle = "Trimestre 3",
                Numero = 3,
                DateDebut = new DateTime(2025, 4, 1),
                DateFin = new DateTime(2025, 6, 30),
                AnneeScolaireId = anneeActuelle.Id,
                CreatedAt = DateTime.Now
            }
        };

        await _context.Periodes.AddRangeAsync(periodes);
    }

    private async Task SeedClasses()
    {
        var classes = new List<Classe>
        {
            // Primaire
            new Classe { Id = Guid.NewGuid(), Nom = "CP", Niveau = "Primaire", Ordre = 1, EffectifMax = 40, CreatedAt = DateTime.Now },
            new Classe { Id = Guid.NewGuid(), Nom = "CE1", Niveau = "Primaire", Ordre = 2, EffectifMax = 40, CreatedAt = DateTime.Now },
            new Classe { Id = Guid.NewGuid(), Nom = "CE2", Niveau = "Primaire", Ordre = 3, EffectifMax = 40, CreatedAt = DateTime.Now },
            new Classe { Id = Guid.NewGuid(), Nom = "CM1", Niveau = "Primaire", Ordre = 4, EffectifMax = 40, CreatedAt = DateTime.Now },
            new Classe { Id = Guid.NewGuid(), Nom = "CM2", Niveau = "Primaire", Ordre = 5, EffectifMax = 40, CreatedAt = DateTime.Now },
            
            // Collège
            new Classe { Id = Guid.NewGuid(), Nom = "6ème", Niveau = "Collège", Ordre = 6, EffectifMax = 45, CreatedAt = DateTime.Now },
            new Classe { Id = Guid.NewGuid(), Nom = "5ème", Niveau = "Collège", Ordre = 7, EffectifMax = 45, CreatedAt = DateTime.Now },
            new Classe { Id = Guid.NewGuid(), Nom = "4ème", Niveau = "Collège", Ordre = 8, EffectifMax = 45, CreatedAt = DateTime.Now },
            new Classe { Id = Guid.NewGuid(), Nom = "3ème", Niveau = "Collège", Ordre = 9, EffectifMax = 45, CreatedAt = DateTime.Now },
            
            // Lycée
            new Classe { Id = Guid.NewGuid(), Nom = "2nde", Niveau = "Lycée", Ordre = 10, EffectifMax = 50, CreatedAt = DateTime.Now },
            new Classe { Id = Guid.NewGuid(), Nom = "1ère", Niveau = "Lycée", Ordre = 11, EffectifMax = 50, CreatedAt = DateTime.Now },
            new Classe { Id = Guid.NewGuid(), Nom = "Terminale", Niveau = "Lycée", Ordre = 12, EffectifMax = 50, CreatedAt = DateTime.Now }
        };

        await _context.Classes.AddRangeAsync(classes);
    }

    private async Task SeedMatieres()
    {
        var matieres = new List<Matiere>
        {
            // Matières communes
            new Matiere { Id = Guid.NewGuid(), Code = "FR", Libelle = "Français", Coefficient = 3, Couleur = "#3B82F6", CreatedAt = DateTime.Now },
            new Matiere { Id = Guid.NewGuid(), Code = "MATH", Libelle = "Mathématiques", Coefficient = 3, Couleur = "#10B981", CreatedAt = DateTime.Now },
            new Matiere { Id = Guid.NewGuid(), Code = "ANG", Libelle = "Anglais", Coefficient = 2, Couleur = "#F59E0B", CreatedAt = DateTime.Now },
            new Matiere { Id = Guid.NewGuid(), Code = "SVT", Libelle = "Sciences de la Vie et de la Terre", Coefficient = 2, Couleur = "#8B5CF6", CreatedAt = DateTime.Now },
            new Matiere { Id = Guid.NewGuid(), Code = "PC", Libelle = "Physique-Chimie", Coefficient = 2, Couleur = "#EF4444", CreatedAt = DateTime.Now },
            new Matiere { Id = Guid.NewGuid(), Code = "HG", Libelle = "Histoire-Géographie", Coefficient = 2, Couleur = "#EC4899", CreatedAt = DateTime.Now },
            new Matiere { Id = Guid.NewGuid(), Code = "EPS", Libelle = "Éducation Physique et Sportive", Coefficient = 1, Couleur = "#06B6D4", CreatedAt = DateTime.Now },
            new Matiere { Id = Guid.NewGuid(), Code = "ART", Libelle = "Arts Plastiques", Coefficient = 1, Couleur = "#F97316", CreatedAt = DateTime.Now },
            new Matiere { Id = Guid.NewGuid(), Code = "INFO", Libelle = "Informatique", Coefficient = 1, Couleur = "#6366F1", CreatedAt = DateTime.Now },
            new Matiere { Id = Guid.NewGuid(), Code = "PHILO", Libelle = "Philosophie", Coefficient = 3, Couleur = "#84CC16", CreatedAt = DateTime.Now }
        };

        await _context.Matieres.AddRangeAsync(matieres);
    }

    private async Task SeedTypesFrais()
    {
        var typesFrais = new List<TypeFrais>
        {
            new TypeFrais
            {
                Id = Guid.NewGuid(),
                Code = "INSC",
                Libelle = "Frais d'inscription",
                Description = "Frais d'inscription annuelle",
                EstRecurrent = false,
                Frequence = "Annuel",
                CreatedAt = DateTime.Now
            },
            new TypeFrais
            {
                Id = Guid.NewGuid(),
                Code = "SCOL",
                Libelle = "Scolarité",
                Description = "Frais de scolarité annuelle",
                EstRecurrent = true,
                Frequence = "Annuel",
                CreatedAt = DateTime.Now
            },
            new TypeFrais
            {
                Id = Guid.NewGuid(),
                Code = "CANT",
                Libelle = "Cantine",
                Description = "Frais de cantine",
                EstRecurrent = true,
                Frequence = "Mensuel",
                CreatedAt = DateTime.Now
            },
            new TypeFrais
            {
                Id = Guid.NewGuid(),
                Code = "TRANS",
                Libelle = "Transport",
                Description = "Frais de transport scolaire",
                EstRecurrent = true,
                Frequence = "Mensuel",
                CreatedAt = DateTime.Now
            },
            new TypeFrais
            {
                Id = Guid.NewGuid(),
                Code = "FOUR",
                Libelle = "Fournitures",
                Description = "Fournitures scolaires",
                EstRecurrent = false,
                Frequence = "Annuel",
                CreatedAt = DateTime.Now
            },
            new TypeFrais
            {
                Id = Guid.NewGuid(),
                Code = "UNIF",
                Libelle = "Uniforme",
                Description = "Uniforme scolaire",
                EstRecurrent = false,
                Frequence = "Annuel",
                CreatedAt = DateTime.Now
            },
            new TypeFrais
            {
                Id = Guid.NewGuid(),
                Code = "EXAM",
                Libelle = "Examens",
                Description = "Frais d'examens officiels",
                EstRecurrent = false,
                Frequence = "Ponctuel",
                CreatedAt = DateTime.Now
            },
            new TypeFrais
            {
                Id = Guid.NewGuid(),
                Code = "ACT",
                Libelle = "Activités parascolaires",
                Description = "Clubs, sorties, activités extra-scolaires",
                EstRecurrent = false,
                Frequence = "Ponctuel",
                CreatedAt = DateTime.Now
            }
        };

        await _context.TypesFrais.AddRangeAsync(typesFrais);
    }
}

// GMS.Infrastructure/Data/Migrations/InitialMigration.cs
// Commande pour générer la migration :
// dotnet ef migrations add InitialCreate --project GMS.Infrastructure --startup-project GMS.Desktop

// Commande pour appliquer la migration :
// dotnet ef database update --project GMS.Infrastructure --startup-project GMS.Desktop

// Script SQL pour backup automatique (à planifier dans SQL Server)
/*
USE [GmsDb]
GO

DECLARE @BackupPath NVARCHAR(500)
DECLARE @BackupName NVARCHAR(500)
DECLARE @Date NVARCHAR(50)

SET @Date = CONVERT(VARCHAR(20), GETDATE(), 112) + '_' + REPLACE(CONVERT(VARCHAR(20), GETDATE(), 108), ':', '')
SET @BackupPath = 'C:\GMS\Backups\'
SET @BackupName = @BackupPath + 'GmsDb_' + @Date + '.bak'

BACKUP DATABASE [GmsDb] 
TO DISK = @BackupName
WITH FORMAT,
     MEDIANAME = 'GmsBackup',
     NAME = 'Full Backup of GmsDb';

-- Supprimer les backups de plus de 30 jours
EXECUTE master.dbo.xp_delete_file 0, @BackupPath, 'bak', GETDATE()-30
GO
*/

// Package.json équivalent - NuGet packages requis
/*
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <!-- Entity Framework Core -->
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0" />
    
    <!-- MediatR (CQRS) -->
    <PackageReference Include="MediatR" Version="12.2.0" />
    
    <!-- FluentValidation -->
    <PackageReference Include="FluentValidation" Version="11.9.0" />
    <PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="11.9.0" />
    
    <!-- BCrypt pour hash password -->
    <PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
    
    <!-- Logging -->
    <PackageReference Include="Serilog" Version="3.1.1" />
    <PackageReference Include="Serilog.Sinks.File" Version="5.0.0" />
    
    <!-- PDF Generation -->
    <PackageReference Include="QuestPDF" Version="2023.12.0" />
    
    <!-- Excel Export -->
    <PackageReference Include="ClosedXML" Version="0.102.1" />
    
    <!-- Barcode/QR Code -->
    <PackageReference Include="QRCoder" Version="1.4.3" />
  </ItemGroup>
</Project>
*/