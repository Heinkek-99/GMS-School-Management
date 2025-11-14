using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMS.Infrastructure.Configurations;

public class ClasseConfiguration : IEntityTypeConfiguration<Classe>
{
    public void Configure(EntityTypeBuilder<Classe> builder)
    {
        builder.ToTable("Classes");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Nom).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Niveau).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Section).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Ordre).IsRequired().HasComment("Ordre d'affichage: 1=CP, 2=CE1, etc.");
        builder.Property(c => c.EffectifMax).IsRequired().HasDefaultValue(30);
        builder.Property(c => c.Libelle).IsRequired().HasMaxLength(100);
        
        // Relations
        // Relation avec AnneeScolaire
        builder.HasOne(c => c.AnneeScolaire)
            .WithMany(a => a.Classes)
            .HasForeignKey(c => c.AnneeScolaireId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        // Relation avec Eleves
        builder.HasMany(c => c.Eleves)
            .WithOne(e => e.Classe)
            .HasForeignKey(e => e.ClasseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relation avec EmploiDuTemps
        builder.HasMany(c => c.EmploiDuTemps)
            .WithOne(e => e.Classe)
            .HasForeignKey(e => e.ClasseId)
            .OnDelete(DeleteBehavior.Cascade);

         // Relation avec Ecole
        builder.HasOne(c => c.Ecole)
            .WithMany(s => s.Classes)
            .HasForeignKey(c => c.EcoleId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();


        // Index
        builder.HasIndex(c => c.Nom).IsUnique();
        builder.HasIndex(c => c.EcoleId);
        builder.HasIndex(c => c.Ordre);
        builder.HasIndex(c => c.AnneeScolaireId);
        builder.HasIndex(c => c.Niveau);
    }
}