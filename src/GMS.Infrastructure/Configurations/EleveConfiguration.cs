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
        builder.Property(e => e.DateNaissance).IsRequired();
        builder.Property(e => e.LieuNaissance).HasMaxLength(150);
        builder.Property(e => e.Sexe).IsRequired().HasMaxLength(1);
        builder.Property(e => e.PhotoPath).HasMaxLength(500);
        builder.Property(e => e.Statut).IsRequired().HasMaxLength(20).HasDefaultValue("Actif");
        builder.Property(e => e.DateInscription).IsRequired();
        builder.Property(e => e.SoldeFinancier).HasPrecision(18, 2).HasDefaultValue(0);


        // Relations
        //Relation avec Ecole
        builder.HasOne(e => e.Ecole)
            .WithMany(s => s.Eleves)
            .HasForeignKey(e => e.EcoleId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        // Relation avec Famille
        builder.HasOne(e => e.Famille)
            .WithMany(f => f.Eleves)
            .HasForeignKey(e => e.FamilleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relation avec Classe
        builder.HasOne(e => e.Classe)
            .WithMany(c => c.Eleves)
            .HasForeignKey(e => e.ClasseId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relation avec Notes
        builder.HasMany(e => e.Notes)
            .WithOne(a => a.Eleve)
            .HasForeignKey(e => e.EleveId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relation avec Frais
        builder.HasMany(e => e.Frais)
            .WithOne(f => f.Eleve)
            .HasForeignKey(f => f.EleveId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Relation avec Presences
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
        builder.HasIndex(e => e.EcoleId);
    }
}