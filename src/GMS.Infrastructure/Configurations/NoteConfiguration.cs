using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMS.Infrastructure.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable("Notes");
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Valeur).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(n => n.NoteSur).HasColumnType("decimal(5,2)").IsRequired().HasDefaultValue(20);
        builder.Property(n => n.TypeEvaluation).HasMaxLength(50);
        builder.Property(n => n.DateEvaluation).IsRequired();

        // Relations
        builder.HasOne(n => n.Eleve)
            .WithMany(e => e.Notes)
            .HasForeignKey(n => n.EleveId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(n => n.Matiere)
            .WithMany(m => m.Notes)
            .HasForeignKey(n => n.MatiereId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(n => n.Periode)
            .WithMany(p => p.Notes)
            .HasForeignKey(n => n.PeriodeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Index
        builder.HasIndex(n => new { n.EleveId, n.MatiereId, n.PeriodeId });
    }
}