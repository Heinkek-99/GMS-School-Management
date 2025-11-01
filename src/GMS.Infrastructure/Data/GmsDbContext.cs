using Microsoft.EntityFrameworkCore;
using GMS.Domain.Entities;

namespace GMS.Infrastructure.Data;

public class GmsDbContext : DbContext
{
    public GmsDbContext(DbContextOptions<GmsDbContext> options) : base(options)
    {
    }

    // DbSets
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

        // Appliquer toutes les configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GmsDbContext).Assembly);

        // Filtres globaux pour soft delete
        modelBuilder.Entity<Utilisateur>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Famille>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Eleve>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Classe>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<AnneeScolaire>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Periode>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<TypeFrais>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Frais>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Paiement>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<VentilationPaiement>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Matiere>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Note>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<EmploiDuTemps>().HasQueryFilter(e => !e.IsDeleted);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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
            }
            else if (entry.State == EntityState.Modified)
            {
                entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}