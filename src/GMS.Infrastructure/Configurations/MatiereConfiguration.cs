using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMS.Infrastructure.Configurations;

public class MatiereConfiguration : IEntityTypeConfiguration<Matiere>
{
    public void Configure(EntityTypeBuilder<Matiere> builder)
    {
        builder.ToTable("Matieres");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Code).IsRequired().HasMaxLength(10);
        builder.Property(m => m.Libelle).IsRequired().HasMaxLength(100);
        builder.Property(m => m.Coefficient);
        builder.Property(m => m.Couleur).HasMaxLength(20);

        // Relations
        builder.HasMany(m => m.Notes)
            .WithOne(n => n.Matiere)
            .HasForeignKey(n => n.MatiereId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.EmploiDuTemps)
            .WithOne(e => e.Matiere)
            .HasForeignKey(e => e.MatiereId)
            .OnDelete(DeleteBehavior.Restrict);

        // Index
        builder.HasIndex(m => m.Code).IsUnique();
    }
}
