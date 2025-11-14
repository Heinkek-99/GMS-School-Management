using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMS.Infrastructure.Configurations;

public class VentilationPaiementConfiguration : IEntityTypeConfiguration<VentilationPaiement>
{
    public void Configure(EntityTypeBuilder<VentilationPaiement> builder)
    {
        builder.ToTable("VentilationsPaiements");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.MontantAffecte)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // Relations
        builder.HasOne(v => v.Paiement)
            .WithMany(p => p.Ventilations)
            .HasForeignKey(v => v.PaiementId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(v => v.Frais)
            .WithMany(f => f.Ventilations)
            .HasForeignKey(v => v.FraisId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index
        builder.HasIndex(v => v.PaiementId);
        builder.HasIndex(v => v.FraisId);
    }
}