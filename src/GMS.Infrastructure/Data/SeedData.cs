using GMS.Domain.Entities;
using GMS.Domain.Entities.Enum;
using GMS.Shared.Constants;
using Microsoft.EntityFrameworkCore;

namespace GMS.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // 1. ÉCOLE (point de départ)
            var ecoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            modelBuilder.Entity<Ecole>().HasData(
                new Ecole
                {
                    Id = ecoleId,
                    Nom = "Complexe Scolaire Bilingue La PRUDENCE",
                    CodeEtablissement = "CSBP-2025",
                    Type = TypeEtablissement.Prive,
                    Telephone = "+237 6 57 34 16 25",
                    Email = "kengneheidy@gmail.com",
                    Adresse = "Maetur Nkomo",
                    Ville = "Yaoundé",
                    Pays = "Cameroun",
                    LogoPath = "/assets/logo.png",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false


                }
            );

            // 2. UTILISATEURS
            var adminId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var directeurId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var secretaireId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var comptableId = Guid.Parse("55555555-5555-5555-5555-555555555555");

            modelBuilder.Entity<Utilisateur>().HasData(
                new Utilisateur
                {
                    Id = adminId,
                    EcoleId = ecoleId,
                    Username = "admin",
                    Email = "admin@excellence-ci.edu",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Nom = "KOUASSI",
                    Prenom = "Jean",
                    Role = Roles.Admin,
                    EstActif = true,
                    PhotoPath = "/assets/users/admin.png",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new Utilisateur
                {
                    Id = directeurId,
                    EcoleId = ecoleId,
                    Username = "directeur",
                    Email = "directeur@excellence-ci.edu",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Dir@123"),
                    Nom = "BAMBA",
                    Prenom = "Marie",
                    Role = Roles.Directeur,
                    EstActif = true,
                    PhotoPath = "/assets/users/director.jpeg",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new Utilisateur
                {
                    Id = secretaireId,
                    EcoleId = ecoleId,
                    Username = "secretaire",
                    Email = "secretaire@excellence-ci.edu",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Sec@123"),
                    Nom = "YAO",
                    Prenom = "Awa",
                    Role = Roles.Secretaire,
                    EstActif = true,
                    PhotoPath = "/assets/users/admin.jpg",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new Utilisateur
                {
                    Id = comptableId,
                    EcoleId = ecoleId,
                    Username = "comptable",
                    Email = "comptable@excellence-ci.edu",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Compta@123"),
                    Nom = "DIALLO",
                    Prenom = "Fatou",
                    Role = Roles.Comptable,
                    EstActif = true,
                    PhotoPath = "/assets/users/sec.png",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                }
            );

            // 3. ANNÉE SCOLAIRE
            var anneeScolaire1Id = Guid.Parse("66666666-6666-6666-6666-666666666661");
            var anneeScolaire2Id = Guid.Parse("66666666-6666-6666-6666-666666666662");

            modelBuilder.Entity<AnneeScolaire>().HasData(
                new AnneeScolaire
                {
                    Id = anneeScolaire1Id,
                    EcoleId = ecoleId,
                    Libelle = "2023-2024",
                    DateDebut = new DateTime(2023, 9, 18),
                    DateFin = new DateTime(2024, 6, 30),
                    EstActive = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },

                new AnneeScolaire
                {
                    Id = anneeScolaire2Id,
                    EcoleId = ecoleId,
                    Libelle = "2024-2025",
                    DateDebut = new DateTime(2024, 9, 16),
                    DateFin = new DateTime(2025, 6, 30),
                    EstActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                }
            );

            // 4. PÉRIODES
            var periode1Id = Guid.Parse("77777777-7777-7777-7777-777777777771");
            var periode2Id = Guid.Parse("77777777-7777-7777-7777-777777777772");
            var periode3Id = Guid.Parse("77777777-7777-7777-7777-777777777773");

            modelBuilder.Entity<Periode>().HasData(
                new Periode
                {
                    Id = periode1Id,
                    AnneeScolaireId = anneeScolaire2Id,
                    Libelle = "1er Trimestre",
                    Numero = 1,
                    DateDebut = new DateTime(2024, 9, 16),
                    DateFin = new DateTime(2024, 12, 20),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new Periode
                {
                    Id = periode2Id,
                    AnneeScolaireId = anneeScolaire2Id,
                    Libelle = "2ème Trimestre",
                    Numero = 2,
                    DateDebut = new DateTime(2025, 1, 6),
                    DateFin = new DateTime(2025, 3, 28),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new Periode
                {
                    Id = periode3Id,
                    AnneeScolaireId = anneeScolaire2Id,
                    Libelle = "3ème Trimestre",
                    Numero = 3,
                    DateDebut = new DateTime(2025, 4, 7),
                    DateFin = new DateTime(2025, 6, 30),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "System",
                    IsDeleted = false
                }
            );

            // 5. CLASSES (Primaire et Collège)

            // Primaire
            var cpId = Guid.Parse("88888888-8888-8888-8888-888888888881");
            var silId = Guid.Parse("88888888-8888-8888-8888-888888888882"); 
            var ce1Id = Guid.Parse("88888888-8888-8888-8888-888888888883");           
            var ce2Id = Guid.Parse("88888888-8888-8888-8888-888888888884");
            var cm1Id = Guid.Parse("88888888-8888-8888-8888-888888888885");
            var cm2Id = Guid.Parse("88888888-8888-8888-8888-888888888886");

            // College
            var sixiemeId = Guid.Parse("88888888-8888-8888-8888-888888888887");
            var cinquiemeId = Guid.Parse("88888888-8888-8888-8888-888888888888");
            var quatriemeId = Guid.Parse("88888888-8888-8888-8888-888888888889");
            var troisiemeId = Guid.Parse("88888888-8888-8888-8888-888888888890");

            //Lycée
            var secondeId = Guid.Parse("88888888-8888-8888-8888-888888888891");
            var premiereId = Guid.Parse("88888888-8888-8888-8888-888888888892");
            var terminaleId = Guid.Parse("88888888-8888-8888-8888-888888888893");



            modelBuilder.Entity<Classe>().HasData(
                new Classe { Id = cpId, EcoleId = ecoleId, AnneeScolaireId = anneeScolaire2Id, Nom = "CP", Libelle = "Cours Préparatoire", Niveau = "Primaire", Section = "A",Ordre = 1, EffectifMax = 35, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
                new Classe { Id = silId, EcoleId = ecoleId, AnneeScolaireId = anneeScolaire2Id, Nom = "SIL", Libelle = "Section d'Initiation Linguistique", Niveau = "Primaire", Section = "A",Ordre = 2, EffectifMax = 30, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
                new Classe { Id = ce1Id, EcoleId = ecoleId, AnneeScolaireId = anneeScolaire2Id, Nom = "CE1", Libelle = "Cours Élémentaire 1", Niveau = "Primaire",Section = "A", Ordre = 3, EffectifMax = 35, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
                new Classe { Id = ce2Id, EcoleId = ecoleId, AnneeScolaireId = anneeScolaire2Id, Nom = "CE2", Libelle = "Cours Élémentaire 2", Niveau = "Primaire", Section = "B",Ordre = 4, EffectifMax = 35, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
                new Classe { Id = cm1Id, EcoleId = ecoleId, AnneeScolaireId = anneeScolaire2Id, Nom = "CM1", Libelle = "Cours Moyen 1", Niveau = "Primaire",Section = "B", Ordre = 5, EffectifMax = 40, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
                new Classe { Id = cm2Id, EcoleId = ecoleId, AnneeScolaireId = anneeScolaire2Id, Nom = "CM2", Libelle = "Cours Moyen 2", Niveau = "Primaire",Section = "S", Ordre = 6, EffectifMax = 40, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },

                new Classe { Id = sixiemeId, EcoleId = ecoleId, AnneeScolaireId = anneeScolaire2Id, Nom = "6EME", Libelle = "Sixième", Niveau = "Collège", Section = "A",Ordre = 7, EffectifMax = 45, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
                new Classe { Id = cinquiemeId, EcoleId = ecoleId, AnneeScolaireId = anneeScolaire2Id, Nom = "5EME", Libelle = "Cinquième", Niveau = "Collège", Section = "B",Ordre = 8, EffectifMax = 45, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
                new Classe { Id = quatriemeId, EcoleId = ecoleId, AnneeScolaireId = anneeScolaire2Id, Nom = "4EME", Libelle = "Quatrième", Niveau = "Collège",Section = "C", Ordre = 9, EffectifMax = 45, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
                new Classe { Id = troisiemeId, EcoleId = ecoleId, AnneeScolaireId = anneeScolaire2Id, Nom = "3EME", Libelle = "Troisième", Niveau = "Collège", Section = "A",Ordre = 10, EffectifMax = 45, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },

                new Classe { Id = secondeId, EcoleId = ecoleId, AnneeScolaireId = anneeScolaire2Id, Nom = "2NDE", Libelle = "Seconde", Niveau = "Lycée", Section = "C1",Ordre = 11, EffectifMax = 50, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
                new Classe { Id = premiereId, EcoleId = ecoleId, AnneeScolaireId = anneeScolaire2Id, Nom = "1ERE", Libelle = "Première", Niveau = "Lycée", Section = "D",Ordre = 12, EffectifMax = 50, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
                new Classe { Id = terminaleId, EcoleId = ecoleId, AnneeScolaireId = anneeScolaire2Id, Nom = "TER", Libelle = "Terminale", Niveau = "Lycée",Section = "D", Ordre = 13, EffectifMax = 50, CreatedAt = DateTime.UtcNow, CreatedBy = "System" }
            );


            // 6. TYPES DE FRAIS
            var InscriptionId = Guid.Parse("99999999-9999-9999-9999-999999999991");
            var scolariteId = Guid.Parse("99999999-9999-9999-9999-999999999992");
            var cantineId = Guid.Parse("99999999-9999-9999-9999-999999999993");
            var transportId = Guid.Parse("99999999-9999-9999-9999-999999999994");

            modelBuilder.Entity<TypeFrais>().HasData(
                new TypeFrais { Id = InscriptionId, EcoleId = ecoleId, Code = "INS", Libelle = "Inscription", Description = "Frais d'inscription annuel", MontantParDefaut = 50000, EstRecurrent = true, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
                new TypeFrais { Id = scolariteId, EcoleId = ecoleId, Code = "SCOL", Libelle = "Scolarité", Description = "Frais de scolarité par trimestre", MontantParDefaut = 150000, EstRecurrent = true, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
                new TypeFrais { Id = cantineId, EcoleId = ecoleId, Code = "CANT", Libelle = "Cantine", Description = "Frais de cantine mensuel", MontantParDefaut = 30000, EstRecurrent = false, CreatedAt = DateTime.UtcNow, CreatedBy = "System" },
                 new TypeFrais { Id = transportId, EcoleId = ecoleId, Code = "TRANS", Libelle = "Transport", Description = "Frais de transport scolaire", MontantParDefaut = 50000, EstRecurrent = false, CreatedAt = DateTime.UtcNow, CreatedBy = "System" }
            );

            // 7. FAMILLES
            var famille1Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var famille2Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            modelBuilder.Entity<Famille>().HasData(
                new Famille
                {
                    Id = famille1Id,
                    EcoleId = ecoleId,
                    CodeFamille = "KH67",
                    NomFamille = "Famille KENGNE",
                    NomResponsable = "KENGNE",
                    PrenomResponsable = "Heidy",
                    NomPere = "KENGNE",
                    NomMere = "NOAH",
                    TelephonePrincipal = "+237 6 57 34 56 78",
                    TelephoneSecondaire = "+237 6 57 65 43 21",
                    Email = "kengneheidy@gmail.com",
                    Adresse = "Tradex biteng",
                    Ville = "Yaoundé",
                    SoldeGlobal = -300000,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false

                },
                new Famille
                {
                    Id = famille2Id,
                    EcoleId = ecoleId,
                    CodeFamille = "KO225",
                    NomFamille = "Famille KOFFI",
                    NomResponsable = "KOFFI",
                    PrenomResponsable = "Adjoua",
                    NomPere = "KOFFI",
                    NomMere = "KANI",
                    TelephonePrincipal = "+225 01 23 45 67 89",
                    Email = "koffi.adjoua@yahoo.fr",
                    Adresse = "Yopougon Sicogi",
                    Ville = "Abidjan",
                    SoldeGlobal = 0,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                }
            );

            // 8. ÉLÈVES
            var eleve1Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
            var eleve2Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
            var eleve3Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");

            modelBuilder.Entity<Eleve>().HasData(
                new Eleve
                {
                    Id = eleve1Id,
                    EcoleId = ecoleId,
                    FamilleId = famille1Id,
                    ClasseId = cm1Id,
                    AnneeScolaireId = anneeScolaire1Id,
                    Matricule = "EXC-2024-001",
                    Nom = "KOUAME",
                    Prenom = "Julien",
                    DateNaissance = new DateTime(2014, 3, 15),
                    LieuNaissance = "Douala",
                    Sexe = "M",
                    Statut = StatutEleve.Actif,
                    DateInscription = new DateTime(2024, 9, 10),
                    SoldeFinancier = -150000,
                    Nationalite = "Tchadien",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new Eleve
                {
                    Id = eleve2Id,
                    EcoleId = ecoleId,
                    FamilleId = famille1Id,
                    ClasseId = ce2Id,
                    AnneeScolaireId = anneeScolaire2Id,
                    Matricule = "EXC-2024-002",
                    Nom = "KAMDEM",
                    Prenom = "Clémence",
                    DateNaissance = new DateTime(2016, 7, 22),
                    LieuNaissance = "Bandjoun",
                    Sexe = "F",
                    Statut = StatutEleve.Actif,
                    DateInscription = new DateTime(2024, 9, 10),
                    SoldeFinancier = -150000,
                    Nationalite = "Camerounais",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new Eleve
                {
                    Id = eleve3Id,
                    EcoleId = ecoleId,
                    FamilleId = famille2Id,
                    ClasseId = sixiemeId,
                    AnneeScolaireId = anneeScolaire2Id,
                    Matricule = "EXC-2024-003",
                    Nom = "KOFFI",
                    Prenom = "Kouadio",
                    DateNaissance = new DateTime(2013, 11, 5),
                    LieuNaissance = "Yamoussoukro",
                    Sexe = "M",
                    Statut = StatutEleve.Actif,
                    DateInscription = new DateTime(2024, 9, 12),
                    SoldeFinancier = 0,
                    Nationalite = "Ivoirien",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    IsDeleted = false
                }
            );

            // 9. FRAIS (Scolarité pour élèves)
            modelBuilder.Entity<Frais>().HasData(

                new Frais
                {
                    Id = Guid.NewGuid(),
                    EleveId = eleve1Id,
                    TypeFraisId = InscriptionId,
                    PeriodeId = periode1Id,
                    Montant = 50000,
                    MontantPaye = 0,
                    Statut = StatutFrais.Impaye,
                    DateEcheance = new DateTime(2024, 10, 31),
                    Observations = "Frais d'inscription annuel CM1",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "System",
                    IsDeleted = false
                },

                new Frais
                {
                    Id = Guid.NewGuid(),
                    EleveId = eleve1Id,
                    TypeFraisId = scolariteId,
                    PeriodeId = periode1Id,
                    Montant = 150000,
                    MontantPaye = 0,
                    Statut = StatutFrais.Impaye,
                    DateEcheance = new DateTime(2024, 10, 31),
                    Observations = "Scolarité 1er Trimestre CM1",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new Frais
                {
                    Id = Guid.NewGuid(),
                    EleveId = eleve2Id,
                    TypeFraisId = scolariteId,
                    PeriodeId = periode1Id,
                    Montant = 150000,
                    MontantPaye = 0,
                    Statut = StatutFrais.Impaye,
                    DateEcheance = new DateTime(2024, 10, 31),
                    Observations = "Scolarité 1er Trimestre CE2",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "System",
                    IsDeleted = false
                },
                new Frais
                {
                    Id = Guid.NewGuid(),
                    EleveId = eleve3Id,
                    TypeFraisId = scolariteId,
                    PeriodeId = periode1Id,
                    Montant = 200000,
                    MontantPaye = 200000,
                    Statut = StatutFrais.Paye,
                    DateEcheance = new DateTime(2024, 10, 31),
                    Observations = "Scolarité 1er Trimestre 6ème",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = "System",
                    IsDeleted = false

                }
            );

            // 10. MATIÈRES (Collège)
            var matieres = new[]
            {
                new Matiere { Id = Guid.NewGuid(), Code = "MATH", Libelle = "Mathématiques", Description = "Mathématiques", Coefficient = 4, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, CreatedBy = "System", UpdatedBy = "System", IsDeleted = false, NoteMin = null   },
                new Matiere { Id = Guid.NewGuid(), Code = "FRAN", Libelle = "Français",Description= "Francais", Coefficient = 4, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, CreatedBy = "System", UpdatedBy = "System", IsDeleted = false, NoteMin = null   },
                new Matiere { Id = Guid.NewGuid(), Code = "ANG", Libelle = "Anglais",Description= "Anglais" ,Coefficient = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, CreatedBy = "System", UpdatedBy = "System", IsDeleted = false, NoteMin = null   },
                new Matiere { Id = Guid.NewGuid(), Code = "SVT", Libelle = "SVT", Description = "Science de la Vie et de la Terre",Coefficient = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, CreatedBy = "System", UpdatedBy = "System", IsDeleted = false, NoteMin = null   },
                new Matiere { Id = Guid.NewGuid(), Code = "HG", Libelle = "Histoire-Géographie", Description = "Histoire et Géographie",Coefficient = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, CreatedBy = "System", UpdatedBy = "System", IsDeleted = false, NoteMin = null   }
            };
            modelBuilder.Entity<Matiere>().HasData(matieres);
        }
    }
}