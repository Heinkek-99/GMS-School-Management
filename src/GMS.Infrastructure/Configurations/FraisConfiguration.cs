using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMS.Infrastructure.Configurations;

public class FraisConfiguration : IEntityTypeConfiguration<Frais>
{
    public void Configure(EntityTypeBuilder<Frais> builder)
    {
        builder.ToTable("Frais");
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Montant).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(f => f.MontantPaye).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(f => f.Statut).IsRequired().HasMaxLength(20).HasDefaultValue("Impayé");
        builder.Property(f => f.Observations).HasMaxLength(500);

        // Relations
        builder.HasOne(f => f.Eleve)
            .WithMany(e => e.Frais)
            .HasForeignKey(f => f.EleveId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(f => f.TypeFrais)
            .WithMany(t => t.Frais)
            .HasForeignKey(f => f.TypeFraisId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(f => f.Ventilations)
            .WithOne(v => v.Frais)
            .HasForeignKey(v => v.FraisId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index
        builder.HasIndex(f => f.EleveId);
        builder.HasIndex(f => f.Statut);
        builder.HasIndex(f => f.DateEcheance);
    }
}