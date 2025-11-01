namespace GMS.Domain.Entities;

public class Matiere : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Libelle { get; set; } = string.Empty;
    public int? Coefficient { get; set; }
    public string? Couleur { get; set; } // Pour UI

    // Navigation
    public ICollection<Note> Notes { get; set; } = new List<Note>();
    public ICollection<EmploiDuTemps> EmploiDuTemps { get; set; } = new List<EmploiDuTemps>();

}