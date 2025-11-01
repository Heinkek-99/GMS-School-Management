namespace GMS.Domain.Entities;

public class Periode : BaseEntity
{
    public string Libelle { get; set; } // Trimestre 1, Semestre 1
    public int Numero { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }

    public Guid AnneeScolaireId { get; set; }
    public AnneeScolaire AnneeScolaire { get; set; }

    // Navigation
    public ICollection<Note> Notes { get; set; } = new List<Note>();

}