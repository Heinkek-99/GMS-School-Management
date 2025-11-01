namespace GMS.Domain.Entities;

public class Note : BaseEntity
{
    public Guid EleveId { get; set; }
    public Eleve Eleve { get; set; } = null;

    public Guid MatiereId { get; set; }
    public Matiere Matiere { get; set; } = null;

    public Guid PeriodeId { get; set; }
    public Periode Periode { get; set; } = null;

    public decimal Valeur { get; set; }
    public decimal NoteSur { get; set; } = 20; // 20, 100, etc.
    public string? TypeEvaluation { get; set; } // Devoir, Composition
    public DateTime DateEvaluation { get; set; }

    // Propriétés calculées
    public decimal NoteSur20 => (Valeur / NoteSur) * 20;
}