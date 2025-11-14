using System.ComponentModel.DataAnnotations.Schema;

namespace GMS.Domain.Entities;

public class Matiere : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Libelle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? Coefficient { get; set; } = 1;
    public string? CouleurAffichage { get; set; } // Pour 
    public decimal HeuresParSemaine { get; set; }
    public string? Categorie { get; set; }
    public decimal? NoteMin { get; set; } 
    public decimal? NoteMax { get; set; } = 20;
    public decimal? SeuilPassage { get; set; } = 10;

    // Navigation
    public ICollection<Note> Notes { get; set; } = new List<Note>();
    public ICollection<EmploiDuTemps> EmploiDuTemps { get; set; } = new List<EmploiDuTemps>();

    [NotMapped]
    public string LibelleAvecCoefficient => $"{Libelle} (Coef. {Coefficient ?? 1})";
}