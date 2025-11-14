using Microsoft.EntityFrameworkCore;
using GMS.Domain.Entities;

using System.Reflection;

namespace GMS.Infrastructure.Data;

public class GmsDbContext : DbContext
{
    public GmsDbContext(DbContextOptions<GmsDbContext> options) : base(options)
    {
    }

    // DbSets
    public DbSet<Ecole> Ecoles { get; set; }
    public DbSet<Presence> Presences { get; set; }
    public DbSet<Utilisateur> Utilisateurs { get; set; }
    public DbSet<Famille> Familles { get; set; }
    public DbSet<Eleve> Eleves { get; set; }
    public DbSet<Classe> Classes { get; set; }
    public DbSet<AnneeScolaire> AnneesScolaires { get; set; }
    public DbSet<Periode> Periodes { get; set; }
    public DbSet<TypeFrais> TypesFrais { get; set; }
    public DbSet<Frais> Frais { get; set; }
    public DbSet<Paiement> Paiements { get; set; }
    public DbSet<VentilationPaiement> VentilationsPaiements { get; set; }
    public DbSet<Matiere> Matieres { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<EmploiDuTemps> EmploisDuTemps { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        // Appliquer toutes les configurations automatiquement
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Appeler les données de seed
        SeedData.Seed(modelBuilder);

        // Configuration globale pour les décimaux (évite les avertissements)
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
                {
                    if (!property.GetPrecision().HasValue)
                    {
                        property.SetPrecision(18);
                        property.SetScale(2);
                    }
                }
            }
        }


        // Configuration globale pour DateTime (UTC par défaut)
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(
                        new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>(
                            v => v.ToUniversalTime(),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Sauvegarde synchrone avec mise à jour automatique des timestamps
    /// </summary>
    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    /// <summary>
    /// Sauvegarde asynchrone avec mise à jour automatique des timestamps
    /// </summary>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Met à jour automatiquement les timestamps (CreatedAt, UpdatedAt) des entités
    /// </summary>
    private void UpdateTimestamps()
    {
        // Mise à jour automatique des timestamps
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity &&
                   (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entity.UpdatedAt = DateTime.UtcNow;
            }
        }

    }
}