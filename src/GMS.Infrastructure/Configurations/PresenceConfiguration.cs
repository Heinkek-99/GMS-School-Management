using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMS.Infrastructure.Configurations;

public class PresenceConfiguration : IEntityTypeConfiguration<Presence>
{
    public void Configure(EntityTypeBuilder<Presence> builder)
    {
        builder.ToTable("Presences");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Date)
            .IsRequired();

        builder.Property(p => p.Statut)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.EnregistrePar).HasMaxLength(100);
        builder.Property(p => p.Commentaire).HasMaxLength(500);

        builder.HasOne(p => p.Eleve)
            .WithMany(e => e.Presences)
            .HasForeignKey(p => p.EleveId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // Indexes for performance
        builder.HasIndex(p => p.EleveId);
        builder.HasIndex(p => p.Date);
        builder.HasIndex(p => p.Statut);
    }

}