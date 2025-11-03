using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMS.Infrastructure.Configurations;

public class EleveConfiguration : IEntityTypeConfiguration<Eleve>
{
    public void Configure(EntityTypeBuilder<Eleve> builder)
    {
        builder.ToTable("Eleves");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Matricule).IsRequired().HasMaxLength(20);
        builder.Property(e => e.Nom).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Prenom).IsRequired().HasMaxLength(100);
        builder.Property(e => e.LieuNaissance).IsRequired().HasMaxLength(150);
        builder.Property(e => e.Sexe).IsRequired().HasMaxLength(1);
        builder.Property(e => e.PhotoPath).HasMaxLength(500);
        builder.Property(e => e.Statut).IsRequired().HasMaxLength(20).HasDefaultValue("Actif");

        // Ignorer les propriétés calculées
        builder.Ignore(e => e.NomComplet);
        builder.Ignore(e => e.Age);
        builder.Ignore(e => e.TotalFrais);
        builder.Ignore(e => e.TotalPaye);
        builder.Ignore(e => e.Solde);

        // Relations
        builder.HasOne(e => e.Famille)
            .WithMany(f => f.Eleves)
            .HasForeignKey(e => e.FamilleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Classe)
            .WithMany(c => c.Eleves)
            .HasForeignKey(e => e.ClasseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.AnneeScolaire)
            .WithMany(a => a.Eleves)
            .HasForeignKey(e => e.AnneeScolaireId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Frais)
            .WithOne(f => f.Eleve)
            .HasForeignKey(f => f.EleveId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Notes)
            .WithOne(n => n.Eleve)
            .HasForeignKey(n => n.EleveId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index
        builder.HasIndex(e => e.Matricule).IsUnique();
        builder.HasIndex(e => new { e.Nom, e.Prenom });
        builder.HasIndex(e => e.FamilleId);
        builder.HasIndex(e => e.ClasseId);
        builder.HasIndex(e => e.Statut);
    }
}