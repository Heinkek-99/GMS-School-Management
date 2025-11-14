using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMS.Infrastructure.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.UtilisateurId).IsRequired();
        builder.Property(a => a.Action).IsRequired().HasMaxLength(20);
        builder.Property(a => a.Entite).IsRequired().HasMaxLength(100);
        builder.Property(a => a.EntiteId).IsRequired();
        builder.Property(a => a.AnciennesValeurs).HasColumnType("nvarchar(max)");
        builder.Property(a => a.NouvellesValeurs).HasColumnType("nvarchar(max)");
        // builder.Property(a => a.AdresseIP).HasMaxLength(50);

        // Configuration de la relation avec Utilisateur
        builder.HasOne(a => a.Utilisateur) 
            .WithMany(u => u.AuditLogs) // Si Utilisateur a une collection d'AuditLogs
            .HasForeignKey(a => a.UtilisateurId) // UtilisateurId doit être une propriété dans AuditLog
            .OnDelete(DeleteBehavior.Restrict);

        // Index
        builder.HasIndex(a => a.UtilisateurId);
        builder.HasIndex(a => a.Action);
        builder.HasIndex(a => a.Entite);
        builder.HasIndex(a => a.CreatedAt);

        // Pas de query filter sur AuditLog (on veut tout garder)

    }
}