using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMS.Infrastructure.Configurations;

public class TypeFraisConfiguration : IEntityTypeConfiguration<TypeFrais>
{
    public void Configure(EntityTypeBuilder<TypeFrais> builder)
    {
        builder.ToTable("TypesFrais");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Code).IsRequired().HasMaxLength(10);
        builder.Property(t => t.Libelle).IsRequired().HasMaxLength(100);
        builder.Property(t => t.Description).HasMaxLength(500);
        builder.Property(t => t.EstRecurrent).IsRequired().HasDefaultValue(false);
        builder.Property(t => t.Frequence).IsRequired().HasMaxLength(20).HasDefaultValue("Annuel");
        builder.Property(t => t.MontantParDefaut).HasColumnType("decimal(18,2)");

        // Relations
        builder.HasMany(t => t.Frais)
            .WithOne(f => f.TypeFrais)
            .HasForeignKey(f => f.TypeFraisId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(tf => tf.Ecole)
            .WithMany(e => e.TypesFrais)
            .HasForeignKey(tf => tf.EcoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Index
        builder.HasIndex(t => t.Code).IsUnique();
    }
}
