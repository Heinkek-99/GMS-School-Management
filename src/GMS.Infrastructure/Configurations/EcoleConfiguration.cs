using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GMS.Domain.Entities;

namespace GMS.Infrastructure.Configurations;

public class EcoleConfiguration : IEntityTypeConfiguration<Ecole>
{
    public void Configure(EntityTypeBuilder<Ecole> builder)
    {
        builder.ToTable("Ecoles");
        builder.HasKey(e => e.Id);

        // Propriétés
        builder.Property(e => e.Nom)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(e => e.CodeEtablissement)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Sigle)
            .HasMaxLength(20);

        builder.Property(e => e.Slogan)
            .HasMaxLength(500);

        builder.Property(e => e.LogoPath)
            .HasMaxLength(500);

        builder.Property(e => e.Telephone)
            .HasMaxLength(20);

        builder.Property(e => e.Email)
            .HasMaxLength(150);

        builder.Property(e => e.SiteWeb)
            .HasMaxLength(200);

        builder.Property(e => e.Adresse)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(e => e.Ville)
            .HasMaxLength(100);

        builder.Property(e => e.CodePostal)
            .HasMaxLength(10);

        builder.Property(e => e.Pays)
            .HasMaxLength(100)
            .HasDefaultValue("Cameroun");

        builder.Property(e => e.NumeroAgrement)
            .HasMaxLength(50);

        builder.Property(e => e.DirecteurNom)
            .HasMaxLength(200);

        builder.Property(e => e.EstActif)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.Devise)
            .HasMaxLength(10)
            .HasDefaultValue("FCFA");

        // Ignorer les propriétés calculées
        builder.Ignore(e => e.NombreEleves);
        builder.Ignore(e => e.NombreClasses);

        // Index
        builder.HasIndex(e => e.Nom);
        builder.HasIndex(e => e.EstActif);

        // Relations
        builder.HasMany(e => e.Utilisateurs)
            .WithOne(u => u.Ecole)
            .HasForeignKey(u => u.EcoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Familles)
            .WithOne(f => f.Ecole)
            .HasForeignKey(f => f.EcoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Eleves)
            .WithOne(el => el.Ecole)
            .HasForeignKey(el => el.EcoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Classes)
            .WithOne(c => c.Ecole)
            .HasForeignKey(c => c.EcoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.AnneesScolaires)
            .WithOne(a => a.Ecole)
            .HasForeignKey(a => a.EcoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.TypesFrais)
            .WithOne(t => t.Ecole)
            .HasForeignKey(t => t.EcoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}