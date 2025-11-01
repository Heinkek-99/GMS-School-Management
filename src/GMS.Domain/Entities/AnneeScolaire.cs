namespace GMS.Domain.Entities;

public class AnneeScolaire : BaseEntity
{
    public string Libelle { get; set; } // 2024-2025
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
    public bool EstActive { get; set; }

    // Navigation
    public ICollection<Eleve> Eleves { get; set; } = new List<Eleve>();
    public ICollection<Periode> Periodes { get; set; } = new List<Periode>();
}