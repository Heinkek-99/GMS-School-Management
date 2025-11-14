namespace GMS.Domain.Entities;

public class Periode : BaseEntity
{
    public string Libelle { get; set; } // Trimestre 1, Semestre 1
    public int Numero { get; set; }

    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }

    public Guid AnneeScolaireId { get; set; }
    public AnneeScolaire AnneeScolaire { get; set; }

    /// Date limite de saisie des notes
    public DateTime? DateLimiteSaisieNotes { get; set; }

    /// Date de conseil de classe
    public DateTime? DateConseilClasse { get; set; }
    
    /// Indique si la période est clôturée (plus de saisie possible)
    public bool EstCloturee { get; set; }

    // Navigation
    public ICollection<Note> Notes { get; set; } = new List<Note>();
    public ICollection<Frais> Frais { get; set; } = new List<Frais>();
    public bool EstEnCours => DateTime.Today >= DateDebut && DateTime.Today <= DateFin;
    public int DureeJours => (DateFin - DateDebut).Days;

}