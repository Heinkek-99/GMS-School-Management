using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GMS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ecoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CodeEtablissement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sigle = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Slogan = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LogoPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    SiteWeb = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Ville = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CodePostal = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Pays = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "Cameroun"),
                    NumeroAgrement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DirecteurNom = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EstActif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Devise = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, defaultValue: "FCFA"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ecoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matieres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coefficient = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CouleurAffichage = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    HeuresParSemaine = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Categorie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NoteMin = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    NoteMax = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    SeuilPassage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matieres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnneesScolaires",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EcoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnneesScolaires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnneesScolaires_Ecoles_EcoleId",
                        column: x => x.EcoleId,
                        principalTable: "Ecoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Familles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomFamille = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodeFamille = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NomResponsable = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrenomResponsable = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TelephonePrincipal = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TelephoneSecondaire = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    NomPere = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrenomPere = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TelephonePere = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmailPere = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    NomMere = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrenomMere = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TelephoneMere = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmailMere = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    SoldeGlobal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    Adresse = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Ville = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CodePostal = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Pays = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "Cameroun"),
                    EcoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Familles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Familles_Ecoles_EcoleId",
                        column: x => x.EcoleId,
                        principalTable: "Ecoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TypesFrais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EstRecurrent = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Frequence = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Annuel"),
                    MontantParDefaut = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    EcoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesFrais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypesFrais_Ecoles_EcoleId",
                        column: x => x.EcoleId,
                        principalTable: "Ecoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EstActif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DerniereConnexion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EcoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Ecoles_EcoleId",
                        column: x => x.EcoleId,
                        principalTable: "Ecoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Niveau = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Section = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ordre = table.Column<int>(type: "int", nullable: false, comment: "Ordre d'affichage: 1=CP, 2=CE1, etc."),
                    EffectifMax = table.Column<int>(type: "int", nullable: false, defaultValue: 30),
                    AnneeScolaireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnseignantPrincipal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelephoneProfesseur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EcoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_AnneesScolaires_AnneeScolaireId",
                        column: x => x.AnneeScolaireId,
                        principalTable: "AnneesScolaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_Ecoles_EcoleId",
                        column: x => x.EcoleId,
                        principalTable: "Ecoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Periodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnneeScolaireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateLimiteSaisieNotes = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateConseilClasse = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstCloturee = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Periodes_AnneesScolaires_AnneeScolaireId",
                        column: x => x.AnneeScolaireId,
                        principalTable: "AnneesScolaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paiements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroPaiement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FamilleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Montant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DatePaiement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModePaiement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EcoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paiements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paiements_Ecoles_EcoleId",
                        column: x => x.EcoleId,
                        principalTable: "Ecoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paiements_Familles_FamilleId",
                        column: x => x.FamilleId,
                        principalTable: "Familles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UtilisateurId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Entite = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnciennesValeurs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NouvellesValeurs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Eleves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Matricule = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LieuNaissance = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Sexe = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateInscription = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Actif"),
                    EcolePrecedente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassePrecedente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentaireScolaire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupeSanguin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Allergies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemeSante = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedecinTraitant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClasseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EcoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnneeScolaireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoldeFinancier = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    Nationalite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eleves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eleves_AnneesScolaires_AnneeScolaireId",
                        column: x => x.AnneeScolaireId,
                        principalTable: "AnneesScolaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Eleves_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eleves_Ecoles_EcoleId",
                        column: x => x.EcoleId,
                        principalTable: "Ecoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Eleves_Familles_FamilleId",
                        column: x => x.FamilleId,
                        principalTable: "Familles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmploisDuTemps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClasseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatiereId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JourSemaine = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    HeureDebut = table.Column<TimeSpan>(type: "time", nullable: false),
                    HeureFin = table.Column<TimeSpan>(type: "time", nullable: false),
                    ProfesseurNom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TypeCours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EcoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CouleurAffichage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploisDuTemps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmploisDuTemps_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmploisDuTemps_Ecoles_EcoleId",
                        column: x => x.EcoleId,
                        principalTable: "Ecoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmploisDuTemps_Matieres_MatiereId",
                        column: x => x.MatiereId,
                        principalTable: "Matieres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Frais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EleveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeFraisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Montant = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DateEcheance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Impayé"),
                    MontantPaye = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    Observations = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Frais_Eleves_EleveId",
                        column: x => x.EleveId,
                        principalTable: "Eleves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Frais_Periodes_PeriodeId",
                        column: x => x.PeriodeId,
                        principalTable: "Periodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Frais_TypesFrais_TypeFraisId",
                        column: x => x.TypeFraisId,
                        principalTable: "TypesFrais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EleveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatiereId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valeur = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false),
                    NoteSur = table.Column<decimal>(type: "decimal(5,2)", precision: 18, scale: 2, nullable: false, defaultValue: 20m),
                    TypeEvaluation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateEvaluation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Intitule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Appreciation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfesseurNom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteMinClasse = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    NoteMaxClasse = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    MoyenneClasse = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    RangClasse = table.Column<int>(type: "int", nullable: true),
                    EstValidee = table.Column<bool>(type: "bit", nullable: false),
                    DateValidation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Eleves_EleveId",
                        column: x => x.EleveId,
                        principalTable: "Eleves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_Matieres_MatiereId",
                        column: x => x.MatiereId,
                        principalTable: "Matieres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notes_Periodes_PeriodeId",
                        column: x => x.PeriodeId,
                        principalTable: "Periodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Presences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EleveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statut = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    HeureArrivee = table.Column<TimeSpan>(type: "time", nullable: true),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentJustificatif = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatiereId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaisiePar = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UtilisateurSaisieId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateSaisie = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DureeRetardMinutes = table.Column<int>(type: "int", nullable: true),
                    EnregistrePar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Commentaire = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ParentsNotifies = table.Column<bool>(type: "bit", nullable: false),
                    DateNotificationParents = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presences_Eleves_EleveId",
                        column: x => x.EleveId,
                        principalTable: "Eleves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Presences_Matieres_MatiereId",
                        column: x => x.MatiereId,
                        principalTable: "Matieres",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Presences_Utilisateurs_UtilisateurSaisieId",
                        column: x => x.UtilisateurSaisieId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VentilationsPaiements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaiementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FraisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MontantAffecte = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentilationsPaiements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VentilationsPaiements_Frais_FraisId",
                        column: x => x.FraisId,
                        principalTable: "Frais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VentilationsPaiements_Paiements_PaiementId",
                        column: x => x.PaiementId,
                        principalTable: "Paiements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ecoles",
                columns: new[] { "Id", "Adresse", "CodeEtablissement", "CodePostal", "CreatedAt", "CreatedBy", "DateCreation", "DeletedAt", "Devise", "DirecteurNom", "Email", "EstActif", "IsDeleted", "LogoPath", "Nom", "NumeroAgrement", "Pays", "Sigle", "SiteWeb", "Slogan", "Telephone", "Type", "UpdatedAt", "UpdatedBy", "Ville" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "Maetur Nkomo", "CSBP-2025", null, new DateTime(2025, 11, 13, 21, 36, 46, 98, DateTimeKind.Utc).AddTicks(1343), "System", null, null, "FCFA", null, "kengneheidy@gmail.com", true, false, "/assets/logo.png", "Complexe Scolaire Bilingue La PRUDENCE", null, "Cameroun", null, null, null, "+237 6 57 34 16 25", 2, new DateTime(2025, 11, 13, 21, 36, 46, 98, DateTimeKind.Utc).AddTicks(1344), "System", "Yaoundé" });

            migrationBuilder.InsertData(
                table: "Matieres",
                columns: new[] { "Id", "Categorie", "Code", "Coefficient", "CouleurAffichage", "CreatedAt", "CreatedBy", "DeletedAt", "Description", "HeuresParSemaine", "IsDeleted", "Libelle", "NoteMax", "NoteMin", "SeuilPassage", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("0c550e50-857e-49bb-94f8-170c097ec0d5"), null, "FRAN", 4, null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4131), "System", null, "Francais", 0m, false, "Français", 20m, null, 10m, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4132), "System" },
                    { new Guid("26f58032-30f3-4990-9208-9d4c52cb02ec"), null, "MATH", 4, null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4119), "System", null, "Mathématiques", 0m, false, "Mathématiques", 20m, null, 10m, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4120), "System" },
                    { new Guid("6fb468d8-bcd8-4ccf-b5f0-b9524481d06e"), null, "ANG", 2, null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4142), "System", null, "Anglais", 0m, false, "Anglais", 20m, null, 10m, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4143), "System" },
                    { new Guid("afff253e-66d1-49b6-a95d-69f02a2a7168"), null, "HG", 2, null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4191), "System", null, "Histoire et Géographie", 0m, false, "Histoire-Géographie", 20m, null, 10m, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4191), "System" },
                    { new Guid("f7b4ebea-a1aa-4f00-a942-d6ee3a78bc87"), null, "SVT", 2, null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4153), "System", null, "Science de la Vie et de la Terre", 0m, false, "SVT", 20m, null, 10m, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4154), "System" }
                });

            migrationBuilder.InsertData(
                table: "AnneesScolaires",
                columns: new[] { "Id", "Commentaire", "CreatedAt", "CreatedBy", "DateDebut", "DateFin", "DeletedAt", "EcoleId", "IsDeleted", "Libelle", "Theme", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("66666666-6666-6666-6666-666666666661"), null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(2833), "System", new DateTime(2023, 9, 17, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 29, 22, 0, 0, 0, DateTimeKind.Utc), null, new Guid("11111111-1111-1111-1111-111111111111"), false, "2023-2024", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(2834), "System" });

            migrationBuilder.InsertData(
                table: "AnneesScolaires",
                columns: new[] { "Id", "Commentaire", "CreatedAt", "CreatedBy", "DateDebut", "DateFin", "DeletedAt", "EcoleId", "EstActive", "IsDeleted", "Libelle", "Theme", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("66666666-6666-6666-6666-666666666662"), null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(2847), "System", new DateTime(2024, 9, 15, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 29, 22, 0, 0, 0, DateTimeKind.Utc), null, new Guid("11111111-1111-1111-1111-111111111111"), true, false, "2024-2025", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(2847), "System" });

            migrationBuilder.InsertData(
                table: "Familles",
                columns: new[] { "Id", "Adresse", "CodeFamille", "CodePostal", "CreatedAt", "CreatedBy", "DeletedAt", "EcoleId", "Email", "EmailMere", "EmailPere", "IsDeleted", "NomFamille", "NomMere", "NomPere", "NomResponsable", "Pays", "PrenomMere", "PrenomPere", "PrenomResponsable", "SoldeGlobal", "TelephoneMere", "TelephonePere", "TelephonePrincipal", "TelephoneSecondaire", "UpdatedAt", "UpdatedBy", "Ville" },
                values: new object[] { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Tradex biteng", "KH67", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3624), "System", null, new Guid("11111111-1111-1111-1111-111111111111"), "kengneheidy@gmail.com", null, null, false, "Famille KENGNE", "NOAH", "KENGNE", "KENGNE", "Cameroun", null, null, "Heidy", -300000m, null, null, "+237 6 57 34 56 78", "+237 6 57 65 43 21", new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3625), "System", "Yaoundé" });

            migrationBuilder.InsertData(
                table: "Familles",
                columns: new[] { "Id", "Adresse", "CodeFamille", "CodePostal", "CreatedAt", "CreatedBy", "DeletedAt", "EcoleId", "Email", "EmailMere", "EmailPere", "IsDeleted", "NomFamille", "NomMere", "NomPere", "NomResponsable", "Pays", "PrenomMere", "PrenomPere", "PrenomResponsable", "TelephoneMere", "TelephonePere", "TelephonePrincipal", "TelephoneSecondaire", "UpdatedAt", "UpdatedBy", "Ville" },
                values: new object[] { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Yopougon Sicogi", "KO225", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3637), "System", null, new Guid("11111111-1111-1111-1111-111111111111"), "koffi.adjoua@yahoo.fr", null, null, false, "Famille KOFFI", "KANI", "KOFFI", "KOFFI", "Cameroun", null, null, "Adjoua", null, null, "+225 01 23 45 67 89", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3638), "System", "Abidjan" });

            migrationBuilder.InsertData(
                table: "TypesFrais",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "DeletedAt", "Description", "EcoleId", "EstRecurrent", "Frequence", "IsDeleted", "Libelle", "MontantParDefaut", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("99999999-9999-9999-9999-999999999991"), "INS", new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3413), "System", null, "Frais d'inscription annuel", new Guid("11111111-1111-1111-1111-111111111111"), true, "Annuel", false, "Inscription", 50000m, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3406), "System" },
                    { new Guid("99999999-9999-9999-9999-999999999992"), "SCOL", new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3427), "System", null, "Frais de scolarité par trimestre", new Guid("11111111-1111-1111-1111-111111111111"), true, "Annuel", false, "Scolarité", 150000m, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3423), "System" }
                });

            migrationBuilder.InsertData(
                table: "TypesFrais",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedBy", "DeletedAt", "Description", "EcoleId", "Frequence", "IsDeleted", "Libelle", "MontantParDefaut", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("99999999-9999-9999-9999-999999999993"), "CANT", new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3435), "System", null, "Frais de cantine mensuel", new Guid("11111111-1111-1111-1111-111111111111"), "Annuel", false, "Cantine", 30000m, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3431), "System" },
                    { new Guid("99999999-9999-9999-9999-999999999994"), "TRANS", new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3445), "System", null, "Frais de transport scolaire", new Guid("11111111-1111-1111-1111-111111111111"), "Annuel", false, "Transport", 50000m, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3439), "System" }
                });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DerniereConnexion", "EcoleId", "Email", "EstActif", "IsDeleted", "Nom", "PasswordHash", "PhotoPath", "Prenom", "Role", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 11, 13, 21, 36, 46, 362, DateTimeKind.Utc).AddTicks(5618), "System", null, null, new Guid("11111111-1111-1111-1111-111111111111"), "admin@excellence-ci.edu", true, false, "KOUASSI", "$2a$11$87n13tcWG/cgFTAFBXH9bugCMBrc3kCmf2mBLJ/hrxBBVLgpaVTOa", "/assets/users/admin.png", "Jean", "Admin", new DateTime(2025, 11, 13, 21, 36, 46, 362, DateTimeKind.Utc).AddTicks(5626), "System", "admin" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 11, 13, 21, 36, 46, 646, DateTimeKind.Utc).AddTicks(7450), "System", null, null, new Guid("11111111-1111-1111-1111-111111111111"), "directeur@excellence-ci.edu", true, false, "BAMBA", "$2a$11$SkLLvLC/TtaiAAySAx4TQOKkzfZorNYqb74G9SOvZnfcnJbcYYKeC", "/assets/users/director.jpeg", "Marie", "Directeur", new DateTime(2025, 11, 13, 21, 36, 46, 646, DateTimeKind.Utc).AddTicks(7455), "System", "directeur" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 11, 13, 21, 36, 46, 923, DateTimeKind.Utc).AddTicks(4227), "System", null, null, new Guid("11111111-1111-1111-1111-111111111111"), "secretaire@excellence-ci.edu", true, false, "YAO", "$2a$11$FdXuSPeud0DQ9MHGYHGiNekOJIrOYvKHpGd8D0SKDKIhMVtNayzqm", "/assets/users/admin.jpg", "Awa", "Secretaire", new DateTime(2025, 11, 13, 21, 36, 46, 923, DateTimeKind.Utc).AddTicks(4233), "System", "secretaire" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(1694), "System", null, null, new Guid("11111111-1111-1111-1111-111111111111"), "comptable@excellence-ci.edu", true, false, "DIALLO", "$2a$11$vZd.Jx57/GuAhrlOA6AXRuGE0c2t/NDKnu1taDAITgahYok1U3WAe", "/assets/users/sec.png", "Fatou", "Comptable", new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(1698), "System", "comptable" }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "AnneeScolaireId", "CreatedAt", "CreatedBy", "DeletedAt", "EcoleId", "EffectifMax", "EnseignantPrincipal", "IsDeleted", "Libelle", "Niveau", "Nom", "Ordre", "Section", "TelephoneProfesseur", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("88888888-8888-8888-8888-888888888881"), new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3130), "System", null, new Guid("11111111-1111-1111-1111-111111111111"), 35, null, false, "Cours Préparatoire", "Primaire", "CP", 1, "A", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3122), "System" },
                    { new Guid("88888888-8888-8888-8888-888888888882"), new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3139), "System", null, new Guid("11111111-1111-1111-1111-111111111111"), 30, null, false, "Section d'Initiation Linguistique", "Primaire", "SIL", 2, "A", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3135), "System" },
                    { new Guid("88888888-8888-8888-8888-888888888883"), new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3147), "System", null, new Guid("11111111-1111-1111-1111-111111111111"), 35, null, false, "Cours Élémentaire 1", "Primaire", "CE1", 3, "A", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3143), "System" },
                    { new Guid("88888888-8888-8888-8888-888888888884"), new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3156), "System", null, new Guid("11111111-1111-1111-1111-111111111111"), 35, null, false, "Cours Élémentaire 2", "Primaire", "CE2", 4, "B", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3153), "System" },
                    { new Guid("88888888-8888-8888-8888-888888888885"), new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3164), "System", null, new Guid("11111111-1111-1111-1111-111111111111"), 40, null, false, "Cours Moyen 1", "Primaire", "CM1", 5, "B", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3161), "System" },
                    { new Guid("88888888-8888-8888-8888-888888888886"), new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3172), "System", null, new Guid("11111111-1111-1111-1111-111111111111"), 40, null, false, "Cours Moyen 2", "Primaire", "CM2", 6, "S", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3167), "System" },
                    { new Guid("88888888-8888-8888-8888-888888888887"), new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3184), "System", null, new Guid("11111111-1111-1111-1111-111111111111"), 45, null, false, "Sixième", "Collège", "6EME", 7, "A", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3181), "System" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3191), "System", null, new Guid("11111111-1111-1111-1111-111111111111"), 45, null, false, "Cinquième", "Collège", "5EME", 8, "B", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3188), "System" },
                    { new Guid("88888888-8888-8888-8888-888888888889"), new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3199), "System", null, new Guid("11111111-1111-1111-1111-111111111111"), 45, null, false, "Quatrième", "Collège", "4EME", 9, "C", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3195), "System" },
                    { new Guid("88888888-8888-8888-8888-888888888890"), new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3251), "System", null, new Guid("11111111-1111-1111-1111-111111111111"), 45, null, false, "Troisième", "Collège", "3EME", 10, "A", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3238), "System" },
                    { new Guid("88888888-8888-8888-8888-888888888891"), new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3259), "System", null, new Guid("11111111-1111-1111-1111-111111111111"), 50, null, false, "Seconde", "Lycée", "2NDE", 11, "C1", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3256), "System" },
                    { new Guid("88888888-8888-8888-8888-888888888892"), new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3267), "System", null, new Guid("11111111-1111-1111-1111-111111111111"), 50, null, false, "Première", "Lycée", "1ERE", 12, "D", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3264), "System" },
                    { new Guid("88888888-8888-8888-8888-888888888893"), new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3275), "System", null, new Guid("11111111-1111-1111-1111-111111111111"), 50, null, false, "Terminale", "Lycée", "TER", 13, "D", null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3272), "System" }
                });

            migrationBuilder.InsertData(
                table: "Periodes",
                columns: new[] { "Id", "AnneeScolaireId", "CreatedAt", "CreatedBy", "DateConseilClasse", "DateDebut", "DateFin", "DateLimiteSaisieNotes", "DeletedAt", "EstCloturee", "IsDeleted", "Libelle", "Numero", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-777777777771"), new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(2979), "System", null, new DateTime(2024, 9, 15, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 12, 19, 23, 0, 0, 0, DateTimeKind.Utc), null, null, false, false, "1er Trimestre", 1, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(2980), "System" },
                    { new Guid("77777777-7777-7777-7777-777777777772"), new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3012), "System", null, new DateTime(2025, 1, 5, 23, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 27, 23, 0, 0, 0, DateTimeKind.Utc), null, null, false, false, "2ème Trimestre", 2, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3013), "System" },
                    { new Guid("77777777-7777-7777-7777-777777777773"), new Guid("66666666-6666-6666-6666-666666666662"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3023), "System", null, new DateTime(2025, 4, 6, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 29, 22, 0, 0, 0, DateTimeKind.Utc), null, null, false, false, "3ème Trimestre", 3, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3024), "System" }
                });

            migrationBuilder.InsertData(
                table: "Eleves",
                columns: new[] { "Id", "Allergies", "AnneeScolaireId", "ClasseId", "ClassePrecedente", "CommentaireScolaire", "CreatedAt", "CreatedBy", "DateInscription", "DateNaissance", "DeletedAt", "EcoleId", "EcolePrecedente", "FamilleId", "GroupeSanguin", "IsDeleted", "LieuNaissance", "Matricule", "MedecinTraitant", "Nationalite", "Nom", "PhotoPath", "Prenom", "ProblemeSante", "Sexe", "SoldeFinancier", "Statut", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, new Guid("66666666-6666-6666-6666-666666666661"), new Guid("88888888-8888-8888-8888-888888888885"), null, null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3842), "System", new DateTime(2024, 9, 9, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2014, 3, 14, 23, 0, 0, 0, DateTimeKind.Utc), null, new Guid("11111111-1111-1111-1111-111111111111"), null, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, false, "Douala", "EXC-2024-001", null, "Tchadien", "KOUAME", null, "Julien", null, "M", -150000m, "Actif", new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3843), "System" },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), null, new Guid("66666666-6666-6666-6666-666666666662"), new Guid("88888888-8888-8888-8888-888888888884"), null, null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3859), "System", new DateTime(2024, 9, 9, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2016, 7, 21, 22, 0, 0, 0, DateTimeKind.Utc), null, new Guid("11111111-1111-1111-1111-111111111111"), null, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, false, "Bandjoun", "EXC-2024-002", null, "Camerounais", "KAMDEM", null, "Clémence", null, "F", -150000m, "Actif", new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3860), "System" }
                });

            migrationBuilder.InsertData(
                table: "Eleves",
                columns: new[] { "Id", "Allergies", "AnneeScolaireId", "ClasseId", "ClassePrecedente", "CommentaireScolaire", "CreatedAt", "CreatedBy", "DateInscription", "DateNaissance", "DeletedAt", "EcoleId", "EcolePrecedente", "FamilleId", "GroupeSanguin", "IsDeleted", "LieuNaissance", "Matricule", "MedecinTraitant", "Nationalite", "Nom", "PhotoPath", "Prenom", "ProblemeSante", "Sexe", "Statut", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null, new Guid("66666666-6666-6666-6666-666666666662"), new Guid("88888888-8888-8888-8888-888888888887"), null, null, new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3871), "System", new DateTime(2024, 9, 11, 22, 0, 0, 0, DateTimeKind.Utc), new DateTime(2013, 11, 4, 23, 0, 0, 0, DateTimeKind.Utc), null, new Guid("11111111-1111-1111-1111-111111111111"), null, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, false, "Yamoussoukro", "EXC-2024-003", null, "Ivoirien", "KOFFI", null, "Kouadio", null, "M", "Actif", new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(3872), "System" });

            migrationBuilder.InsertData(
                table: "Frais",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DateEcheance", "DeletedAt", "EleveId", "IsDeleted", "Montant", "Observations", "PeriodeId", "Statut", "TypeFraisId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("46b22afd-4122-4759-9657-0fdae4d30f15"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4068), "System", new DateTime(2024, 10, 30, 23, 0, 0, 0, DateTimeKind.Utc), null, new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), false, 150000m, "Scolarité 1er Trimestre CE2", new Guid("77777777-7777-7777-7777-777777777771"), "Impayé", new Guid("99999999-9999-9999-9999-999999999992"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4069), "System" },
                    { new Guid("605e5368-1ab0-45e5-85cf-031beb92fddc"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4046), "System", new DateTime(2024, 10, 30, 23, 0, 0, 0, DateTimeKind.Utc), null, new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), false, 50000m, "Frais d'inscription annuel CM1", new Guid("77777777-7777-7777-7777-777777777771"), "Impayé", new Guid("99999999-9999-9999-9999-999999999991"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4047), "System" },
                    { new Guid("78f63e09-a1e5-4eef-bba4-2adfcb28b51a"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4057), "System", new DateTime(2024, 10, 30, 23, 0, 0, 0, DateTimeKind.Utc), null, new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), false, 150000m, "Scolarité 1er Trimestre CM1", new Guid("77777777-7777-7777-7777-777777777771"), "Impayé", new Guid("99999999-9999-9999-9999-999999999992"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4059), "System" }
                });

            migrationBuilder.InsertData(
                table: "Frais",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DateEcheance", "DeletedAt", "EleveId", "IsDeleted", "Montant", "MontantPaye", "Observations", "PeriodeId", "Statut", "TypeFraisId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("be362e16-42b4-4118-9265-511bc4ac5dc3"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4080), "System", new DateTime(2024, 10, 30, 23, 0, 0, 0, DateTimeKind.Utc), null, new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), false, 200000m, 200000m, "Scolarité 1er Trimestre 6ème", new Guid("77777777-7777-7777-7777-777777777771"), "Payé", new Guid("99999999-9999-9999-9999-999999999992"), new DateTime(2025, 11, 13, 21, 36, 47, 224, DateTimeKind.Utc).AddTicks(4081), "System" });

            migrationBuilder.CreateIndex(
                name: "IX_AnneesScolaires_EcoleId",
                table: "AnneesScolaires",
                column: "EcoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AnneesScolaires_EstActive",
                table: "AnneesScolaires",
                column: "EstActive");

            migrationBuilder.CreateIndex(
                name: "IX_AnneesScolaires_Libelle",
                table: "AnneesScolaires",
                column: "Libelle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Action",
                table: "AuditLogs",
                column: "Action");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_CreatedAt",
                table: "AuditLogs",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Entite",
                table: "AuditLogs",
                column: "Entite");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UtilisateurId",
                table: "AuditLogs",
                column: "UtilisateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_AnneeScolaireId",
                table: "Classes",
                column: "AnneeScolaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_EcoleId",
                table: "Classes",
                column: "EcoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_Niveau",
                table: "Classes",
                column: "Niveau");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_Nom",
                table: "Classes",
                column: "Nom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_Ordre",
                table: "Classes",
                column: "Ordre");

            migrationBuilder.CreateIndex(
                name: "IX_Ecoles_EstActif",
                table: "Ecoles",
                column: "EstActif");

            migrationBuilder.CreateIndex(
                name: "IX_Ecoles_Nom",
                table: "Ecoles",
                column: "Nom");

            migrationBuilder.CreateIndex(
                name: "IX_Eleves_AnneeScolaireId",
                table: "Eleves",
                column: "AnneeScolaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Eleves_ClasseId",
                table: "Eleves",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_Eleves_EcoleId",
                table: "Eleves",
                column: "EcoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Eleves_FamilleId",
                table: "Eleves",
                column: "FamilleId");

            migrationBuilder.CreateIndex(
                name: "IX_Eleves_Matricule",
                table: "Eleves",
                column: "Matricule",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Eleves_Nom_Prenom",
                table: "Eleves",
                columns: new[] { "Nom", "Prenom" });

            migrationBuilder.CreateIndex(
                name: "IX_Eleves_Statut",
                table: "Eleves",
                column: "Statut");

            migrationBuilder.CreateIndex(
                name: "IX_EmploisDuTemps_ClasseId_JourSemaine_HeureDebut",
                table: "EmploisDuTemps",
                columns: new[] { "ClasseId", "JourSemaine", "HeureDebut" });

            migrationBuilder.CreateIndex(
                name: "IX_EmploisDuTemps_EcoleId",
                table: "EmploisDuTemps",
                column: "EcoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploisDuTemps_MatiereId",
                table: "EmploisDuTemps",
                column: "MatiereId");

            migrationBuilder.CreateIndex(
                name: "IX_Familles_EcoleId",
                table: "Familles",
                column: "EcoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Familles_IsDeleted",
                table: "Familles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Familles_NomResponsable_PrenomResponsable",
                table: "Familles",
                columns: new[] { "NomResponsable", "PrenomResponsable" });

            migrationBuilder.CreateIndex(
                name: "IX_Familles_TelephonePrincipal",
                table: "Familles",
                column: "TelephonePrincipal");

            migrationBuilder.CreateIndex(
                name: "IX_Frais_DateEcheance",
                table: "Frais",
                column: "DateEcheance");

            migrationBuilder.CreateIndex(
                name: "IX_Frais_EleveId",
                table: "Frais",
                column: "EleveId");

            migrationBuilder.CreateIndex(
                name: "IX_Frais_PeriodeId",
                table: "Frais",
                column: "PeriodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Frais_Statut",
                table: "Frais",
                column: "Statut");

            migrationBuilder.CreateIndex(
                name: "IX_Frais_TypeFraisId",
                table: "Frais",
                column: "TypeFraisId");

            migrationBuilder.CreateIndex(
                name: "IX_Matieres_Categorie",
                table: "Matieres",
                column: "Categorie");

            migrationBuilder.CreateIndex(
                name: "IX_Matieres_Code",
                table: "Matieres",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_EleveId_MatiereId_PeriodeId",
                table: "Notes",
                columns: new[] { "EleveId", "MatiereId", "PeriodeId" });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_MatiereId",
                table: "Notes",
                column: "MatiereId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_PeriodeId",
                table: "Notes",
                column: "PeriodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Paiements_DatePaiement",
                table: "Paiements",
                column: "DatePaiement");

            migrationBuilder.CreateIndex(
                name: "IX_Paiements_EcoleId",
                table: "Paiements",
                column: "EcoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Paiements_FamilleId",
                table: "Paiements",
                column: "FamilleId");

            migrationBuilder.CreateIndex(
                name: "IX_Paiements_NumeroPaiement",
                table: "Paiements",
                column: "NumeroPaiement",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Periodes_AnneeScolaireId_Numero",
                table: "Periodes",
                columns: new[] { "AnneeScolaireId", "Numero" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Presences_Date",
                table: "Presences",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Presences_EleveId",
                table: "Presences",
                column: "EleveId");

            migrationBuilder.CreateIndex(
                name: "IX_Presences_MatiereId",
                table: "Presences",
                column: "MatiereId");

            migrationBuilder.CreateIndex(
                name: "IX_Presences_Statut",
                table: "Presences",
                column: "Statut");

            migrationBuilder.CreateIndex(
                name: "IX_Presences_UtilisateurSaisieId",
                table: "Presences",
                column: "UtilisateurSaisieId");

            migrationBuilder.CreateIndex(
                name: "IX_TypesFrais_Code",
                table: "TypesFrais",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypesFrais_EcoleId",
                table: "TypesFrais",
                column: "EcoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_EcoleId",
                table: "Utilisateurs",
                column: "EcoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_Email",
                table: "Utilisateurs",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_EstActif",
                table: "Utilisateurs",
                column: "EstActif");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_Username",
                table: "Utilisateurs",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VentilationsPaiements_FraisId",
                table: "VentilationsPaiements",
                column: "FraisId");

            migrationBuilder.CreateIndex(
                name: "IX_VentilationsPaiements_PaiementId",
                table: "VentilationsPaiements",
                column: "PaiementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "EmploisDuTemps");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Presences");

            migrationBuilder.DropTable(
                name: "VentilationsPaiements");

            migrationBuilder.DropTable(
                name: "Matieres");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Frais");

            migrationBuilder.DropTable(
                name: "Paiements");

            migrationBuilder.DropTable(
                name: "Eleves");

            migrationBuilder.DropTable(
                name: "Periodes");

            migrationBuilder.DropTable(
                name: "TypesFrais");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Familles");

            migrationBuilder.DropTable(
                name: "AnneesScolaires");

            migrationBuilder.DropTable(
                name: "Ecoles");
        }
    }
}
