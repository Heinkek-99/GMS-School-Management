using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMS.Infrastructure.Configurations;

public class FamilleConfiguration : IEntityTypeConfiguration<Famille>
{
    public void Configure(EntityTypeBuilder<Famille> builder)
    {
        builder.ToTable("Familles");
        builder.HasKey(f => f.Id);

        builder.Property(f => f.CodeFamille).IsRequired().HasMaxLength(20);
        builder.Property(f => f.NomFamille).IsRequired().HasMaxLength(100);
        builder.Property(f => f.NomResponsable).IsRequired().HasMaxLength(100);
        builder.Property(f => f.PrenomResponsable).HasMaxLength(100);
        builder.Property(f => f.TelephonePrincipal).HasMaxLength(20);
        builder.Property(f => f.TelephoneSecondaire).HasMaxLength(20);
        builder.Property(f => f.Email).HasMaxLength(150);
        builder.Property(f => f.NomPere).IsRequired().HasMaxLength(100);
        builder.Property(f => f.PrenomPere).HasMaxLength(100);
        builder.Property(f => f.TelephonePere).HasMaxLength(20);
        builder.Property(f => f.EmailPere).HasMaxLength(150);
        builder.Property(f => f.NomMere).IsRequired().HasMaxLength(100);
        builder.Property(f => f.PrenomMere).HasMaxLength(100);
        builder.Property(f => f.TelephoneMere).HasMaxLength(20);
        builder.Property(f => f.EmailMere).HasMaxLength(150);
        builder.Property(f => f.Adresse).IsRequired().HasMaxLength(250);
        builder.Property(f => f.Ville).HasMaxLength(100);
        builder.Property(f => f.CodePostal).HasMaxLength(10);
        builder.Property(f => f.SoldeGlobal).HasPrecision(18,2).HasDefaultValue(0);
        builder.Property(f => f.Pays).HasMaxLength(100).HasDefaultValue("Cameroun");

        // Relations
        builder.HasOne(f => f.Ecole)
            .WithMany(s => s.Familles)
            .HasForeignKey(f => f.EcoleId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasMany(f => f.Eleves)
            .WithOne(e => e.Famille)
            .HasForeignKey(e => e.FamilleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(f => f.Paiements)
            .WithOne(p => p.Famille)
            .HasForeignKey(p => p.FamilleId)
            .OnDelete(DeleteBehavior.Restrict);

        // 
        builder.HasIndex(f => f.EcoleId);
        builder.HasIndex(f => new{f.NomResponsable, f.PrenomResponsable});
        builder.HasIndex(f => f.TelephonePrincipal);
        builder.HasIndex(f => f.IsDeleted);
    }
}