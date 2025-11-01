using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMS.Infrastructure.Configurations;

public class AnneeScolaireConfiguration : IEntityTypeConfiguration<AnneeScolaire>
{
    public void Configure(EntityTypeBuilder<AnneeScolaire> builder)
    {
        builder.ToTable("AnneesScolaires");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Libelle).IsRequired().HasMaxLength(20);
        builder.Property(a => a.DateDebut).IsRequired();
        builder.Property(a => a.DateFin).IsRequired();
        builder.Property(a => a.EstActive).IsRequired().HasDefaultValue(false);

        // Relations
        builder.HasMany(a => a.Eleves)
            .WithOne(e => e.AnneeScolaire)
            .HasForeignKey(e => e.AnneeScolaireId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Periodes)
            .WithOne(p => p.AnneeScolaire)
            .HasForeignKey(p => p.AnneeScolaireId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index
        builder.HasIndex(a => a.Libelle).IsUnique();
        builder.HasIndex(a => a.EstActive);
    }
}
