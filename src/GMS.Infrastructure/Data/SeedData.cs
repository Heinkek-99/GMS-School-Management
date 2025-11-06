using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GMS.Domain.Entities;
using BCrypt.Net;

namespace GMS.Infrastructure.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<GmsDbContext>();

        // Appliquer les migrations
        await context.Database.MigrateAsync();

        // Seed Utilisateurs
        await SeedUtilisateurs(context);

        // Seed Année Scolaire
        await SeedAnneeScolaire(context);

        // Seed Classes
        await SeedClasses(context);

        // Seed Types de Frais
        await SeedTypesFrais(context);

        // Seed Matières
        await SeedMatieres(context);

        await context.SaveChangesAsync();
    }

    private static async Task SeedUtilisateurs(GmsDbContext context)
    {
        if (await context.Utilisateurs.AnyAsync())
            return;

        var utilisateurs = new List<Utilisateur>
        {
            new Utilisateur
            {
                Id = Guid.NewGuid(),
                Nom = "Admin",
                Prenom = "Super",
                Email = "admin@gms.com",
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                Role = "Admin",
                EstActif = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new Utilisateur
            {
                Id = Guid.NewGuid(),
                Nom = "KOFFI",
                Prenom = "Marie",
                Email = "directeur@gms.com",
                Username = "directeur",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Direct@123"),
                Role = "Directeur",
                EstActif = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new Utilisateur
            {
                Id = Guid.NewGuid(),
                Nom = "TRAORE",
                Prenom = "Fatou",
                Email = "secretaire@gms.com",
                Username = "secretaire",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Secret@123"),
                Role = "Secretaire",
                EstActif = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new Utilisateur
            {
                Id = Guid.NewGuid(),
                Nom = "OUATTARA",
                Prenom = "Ibrahim",
                Email = "comptable@gms.com",
                Username = "comptable",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Compta@123"),
                Role = "Comptable",
                EstActif = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            }
        };

        await context.Utilisateurs.AddRangeAsync(utilisateurs);
    }

    private static async Task SeedAnneeScolaire(GmsDbContext context)
    {
        if (await context.AnneesScolaires.AnyAsync())
            return;

        var annees = new List<AnneeScolaire>
        {
            new AnneeScolaire
            {
                Id = Guid.NewGuid(),
                Libelle = "2024-2025",
                DateDebut = new DateTime(2024, 9, 1),
                DateFin = new DateTime(2025, 6, 30),
                EstActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new AnneeScolaire
            {
                Id = Guid.NewGuid(),
                Libelle = "2023-2024",
                DateDebut = new DateTime(2023, 9, 1),
                DateFin = new DateTime(2024, 6, 30),
                EstActive = false,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            }
        };

        await context.AnneesScolaires.AddRangeAsync(annees);

        // Ajouter les périodes (trimestres)
        var anneeActive = annees.First(a => a.EstActive);
        var periodes = new List<Periode>
        {
            new Periode
            {
                Id = Guid.NewGuid(),
                Libelle = "Trimestre 1",
                Numero = 1,
                DateDebut = new DateTime(2024, 9, 1),
                DateFin = new DateTime(2024, 12, 20),
                AnneeScolaireId = anneeActive.Id,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new Periode
            {
                Id = Guid.NewGuid(),
                Libelle = "Trimestre 2",
                Numero = 2,
                DateDebut = new DateTime(2025, 1, 7),
                DateFin = new DateTime(2025, 3, 31),
                AnneeScolaireId = anneeActive.Id,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new Periode
            {
                Id = Guid.NewGuid(),
                Libelle = "Trimestre 3",
                Numero = 3,
                DateDebut = new DateTime(2025, 4, 1),
                DateFin = new DateTime(2025, 6, 30),
                AnneeScolaireId = anneeActive.Id,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            }
        };

        await context.Periodes.AddRangeAsync(periodes);
    }

    private static async Task SeedClasses(GmsDbContext context)
    {
        if (await context.Classes.AnyAsync())
            return;

        var classes = new List<Classe>
        {
            // Primaire
            new Classe { Id = Guid.NewGuid(), Nom = "CP", Niveau = "Primaire", Ordre = 1, EffectifMax = 30, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
            new Classe { Id = Guid.NewGuid(), Nom = "CE1", Niveau = "Primaire", Ordre = 2, EffectifMax = 30, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
            new Classe { Id = Guid.NewGuid(), Nom = "CE2", Niveau = "Primaire", Ordre = 3, EffectifMax = 30, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
            new Classe { Id = Guid.NewGuid(), Nom = "CM1", Niveau = "Primaire", Ordre = 4, EffectifMax = 35, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
            new Classe { Id = Guid.NewGuid(), Nom = "CM2", Niveau = "Primaire", Ordre = 5, EffectifMax = 35, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },

            // Collège
            new Classe { Id = Guid.NewGuid(), Nom = "6ème", Niveau = "Collège", Ordre = 6, EffectifMax = 40, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
            new Classe { Id = Guid.NewGuid(), Nom = "5ème", Niveau = "Collège", Ordre = 7, EffectifMax = 40, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
            new Classe { Id = Guid.NewGuid(), Nom = "4ème", Niveau = "Collège", Ordre = 8, EffectifMax = 40, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
            new Classe { Id = Guid.NewGuid(), Nom = "3ème", Niveau = "Collège", Ordre = 9, EffectifMax = 40, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },

            // Lycée
            new Classe { Id = Guid.NewGuid(), Nom = "2nde", Niveau = "Lycée", Ordre = 10, EffectifMax = 45, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
            new Classe { Id = Guid.NewGuid(), Nom = "1ère", Niveau = "Lycée", Ordre = 11, EffectifMax = 45, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
            new Classe { Id = Guid.NewGuid(), Nom = "Terminale", Niveau = "Lycée", Ordre = 12, EffectifMax = 45, CreatedAt = DateTime.UtcNow, CreatedBy = "System" }
        };

        await context.Classes.AddRangeAsync(classes);
    }

    private static async Task SeedTypesFrais(GmsDbContext context)
    {
        if (await context.TypesFrais.AnyAsync())
            return;

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
                MontantParDefaut = 50000,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new TypeFrais
            {
                Id = Guid.NewGuid(),
                Code = "SCOL",
                Libelle = "Scolarité",
                Description = "Frais de scolarité mensuelle",
                EstRecurrent = true,
                Frequence = "Mensuel",
                MontantParDefaut = 75000,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new TypeFrais
            {
                Id = Guid.NewGuid(),
                Code = "CANT",
                Libelle = "Cantine",
                Description = "Frais de cantine mensuelle",
                EstRecurrent = true,
                Frequence = "Mensuel",
                MontantParDefaut = 25000,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new TypeFrais
            {
                Id = Guid.NewGuid(),
                Code = "UNIF",
                Libelle = "Uniforme",
                Description = "Frais d'uniforme scolaire",
                EstRecurrent = false,
                Frequence = "Annuel",
                MontantParDefaut = 35000,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new TypeFrais
            {
                Id = Guid.NewGuid(),
                Code = "MANU",
                Libelle = "Manuels scolaires",
                Description = "Fournitures et manuels",
                EstRecurrent = false,
                Frequence = "Annuel",
                MontantParDefaut = 40000,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            },
            new TypeFrais
            {
                Id = Guid.NewGuid(),
                Code = "TRAN",
                Libelle = "Transport",
                Description = "Frais de transport scolaire",
                EstRecurrent = true,
                Frequence = "Mensuel",
                MontantParDefaut = 30000,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            }
        };

        await context.TypesFrais.AddRangeAsync(typesFrais);
    }

    private static async Task SeedMatieres(GmsDbContext context)
    {
        if (await context.Matieres.AnyAsync())
            return;

        var matieres = new List<Matiere>
        {
            new Matiere { Id = Guid.NewGuid(), Code = "FR", Libelle = "Français", Coefficient = 4, Couleur = "#3B82F6", CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
            new Matiere { Id = Guid.NewGuid(), Code = "MATH", Libelle = "Mathématiques", Coefficient = 4, Couleur = "#10B981", CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
            new Matiere { Id = Guid.NewGuid(), Code = "ANG", Libelle = "Anglais", Coefficient = 3, Couleur = "#F59E0B", CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
            new Matiere { Id = Guid.NewGuid(), Code = "HG", Libelle = "Histoire-Géographie", Coefficient = 3, Couleur = "#EF4444", CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
            new Matiere { Id = Guid.NewGuid(), Code = "SVT", Libelle = "Sciences de la Vie et de la Terre", Coefficient = 3, Couleur = "#8B5CF6", CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
            new Matiere { Id = Guid.NewGuid(), Code = "PC", Libelle = "Physique-Chimie", Coefficient = 3, Couleur = "#EC4899", CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
            new Matiere { Id = Guid.NewGuid(), Code = "EPS", Libelle = "Éducation Physique et Sportive", Coefficient = 2, Couleur = "#14B8A6", CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
            new Matiere { Id = Guid.NewGuid(), Code = "INFO", Libelle = "Informatique", Coefficient = 2, Couleur = "#6366F1", CreatedAt = DateTime.UtcNow, CreatedBy = "System" }
        };

        await context.Matieres.AddRangeAsync(matieres);
    }
}