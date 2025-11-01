using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMS.Infrastructure.Configurations;

public class PaiementConfiguration : IEntityTypeConfiguration<Paiement>
{
    public void Configure(EntityTypeBuilder<Paiement> builder)
    {
        builder.ToTable("Paiements");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.NumeroPaiement).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Montant).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(p => p.ModePaiement).IsRequired().HasMaxLength(50);
        builder.Property(p => p.NumeroReference).HasMaxLength(100);
        builder.Property(p => p.Observations).HasMaxLength(500);

        // Relations
        builder.HasOne(p => p.Famille)
            .WithMany(f => f.Paiements)
            .HasForeignKey(p => p.FamilleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Ventilations)
            .WithOne(v => v.Paiement)
            .HasForeignKey(v => v.PaiementId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index
        builder.HasIndex(p => p.NumeroPaiement).IsUnique();
        builder.HasIndex(p => p.FamilleId);
        builder.HasIndex(p => p.DatePaiement);
    }
}