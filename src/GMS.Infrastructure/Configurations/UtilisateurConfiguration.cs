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
        builder.Property(u => u.EstActif).HasDefaultValue(true);

        builder.Ignore(u => u.NomComplet);
        
        // Index
        builder.HasIndex(u => u.Username).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
    }
}