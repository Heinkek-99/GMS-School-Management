using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMS.Infrastructure.Configurations;

public class EmploiDuTempsConfiguration : IEntityTypeConfiguration<EmploiDuTemps>
{
    public void Configure(EntityTypeBuilder<EmploiDuTemps> builder)
    {
        builder.ToTable("EmploisDuTemps");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.JourSemaine).IsRequired().HasMaxLength(20);
        builder.Property(e => e.HeureDebut).IsRequired();
        builder.Property(e => e.HeureFin).IsRequired();
        builder.Property(e => e.Salle).HasMaxLength(50);

        // Relations
        builder.HasOne(e => e.Classe)
            .WithMany(c => c.EmploiDuTemps)
            .HasForeignKey(e => e.ClasseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Matiere)
            .WithMany(m => m.EmploiDuTemps)
            .HasForeignKey(e => e.MatiereId)
            .OnDelete(DeleteBehavior.Restrict);

        // Index
        builder.HasIndex(e => new { e.ClasseId, e.JourSemaine, e.HeureDebut });
    }
}
