using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GMS.Domain.Entities;

namespace GMS.Infrastructure.Configurations;

public class AnneeScolaireConfiguration : IEntityTypeConfiguration<AnneeScolaire>
{
    public void Configure(EntityTypeBuilder<AnneeScolaire> builder)
    {
        builder.ToTable("AnneesScolaires");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Libelle)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.DateDebut)
            .IsRequired();

        builder.Property(a => a.DateFin)
            .IsRequired();

        builder.Property(a => a.EstActive)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.Property(a => a.UpdatedAt)
            .IsRequired(false);

        builder.Property(a => a.CreatedBy)
            .IsRequired();

        builder.Property(a => a.UpdatedBy)
            .IsRequired(false);

        builder.Property(a => a.IsDeleted)
            .IsRequired();

        // Index sur Libelle (unique) et EstActive
        builder.HasIndex(a => a.Libelle).IsUnique();
        builder.HasIndex(a => a.EstActive);

        // Relation avec Ecole
        builder.HasOne(a => a.Ecole)
            .WithMany(e => e.AnneesScolaires)
            .HasForeignKey(a => a.EcoleId)
            .OnDelete(DeleteBehavior.Restrict);
        
    }
}
