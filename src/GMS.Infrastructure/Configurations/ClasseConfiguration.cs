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
        builder.Property(c => c.Ordre).IsRequired();

        // Relations
        builder.HasMany(c => c.Eleves)
            .WithOne(e => e.Classe)
            .HasForeignKey(e => e.ClasseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.EmploiDuTemps)
            .WithOne(e => e.Classe)
            .HasForeignKey(e => e.ClasseId)
            .OnDelete(DeleteBehavior.Cascade);

        //Ignorer les propriétés calculées
        builder.Ignore(c => c.EffectifActuel);
        builder.Ignore(c => c.EstPleine);

        // Index
        builder.HasIndex(c => c.Nom).IsUnique();
        builder.HasIndex(c => c.Ordre);
        builder.HasIndex(c => c.Niveau);
    }
}