using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMS.Infrastructure.Configurations;

public class UtilisateurConfiguration : IEntityTypeConfiguration<Utilisateur>
{
    public void Configure(EntityTypeBuilder<Utilisateur> builder)
    {
        builder.ToTable("Utilisateurs");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nom).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Prenom).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(150);
        builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(250);
        builder.Property(u => u.Role).IsRequired().HasMaxLength(50);
        builder.Property(u => u.EstActif).IsRequired().HasDefaultValue(true);
        builder.Property(u => u.PhotoPath).HasMaxLength(200);

        builder.HasOne(u => u.Ecole)
            .WithMany(s => s.Utilisateurs)
            .HasForeignKey(u => u.EcoleId)
            .OnDelete(DeleteBehavior.Restrict) // Empêche suppression école si utilisateurs existent
            .IsRequired();

        // Index pour performance
        builder.HasIndex(u => u.Username).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.EcoleId);
        builder.HasIndex(u => u.EstActif);
    }
}