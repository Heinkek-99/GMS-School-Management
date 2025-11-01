using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMS.Infrastructure.Configurations;

public class PeriodeConfiguration : IEntityTypeConfiguration<Periode>
{
    public void Configure(EntityTypeBuilder<Periode> builder)
    {
        builder.ToTable("Periodes");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Libelle).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Numero).IsRequired();
        builder.Property(p => p.DateDebut).IsRequired();
        builder.Property(p => p.DateFin).IsRequired();

        // Relations
        builder.HasOne(p => p.AnneeScolaire)
            .WithMany(a => a.Periodes)
            .HasForeignKey(p => p.AnneeScolaireId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.Notes)
            .WithOne(n => n.Periode)
            .HasForeignKey(n => n.PeriodeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Index
        builder.HasIndex(p => new { p.AnneeScolaireId, p.Numero }).IsUnique();
    }
}