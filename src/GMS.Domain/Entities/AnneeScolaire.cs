using System.ComponentModel.DataAnnotations.Schema;

namespace GMS.Domain.Entities;

public class AnneeScolaire : BaseEntity
{
    public string Libelle { get; set; } // 2024-2025
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
    public bool EstActive { get; set; }

    public Guid EcoleId { get; set; }
    public Ecole Ecole { get; set; } = null!;

    public string? Theme { get; set; }
    public string? Commentaire { get; set; }

    // Navigation
    public ICollection<Classe> Classes { get; set; } = new List<Classe>();
    public ICollection<Periode> Periodes { get; set; } = new List<Periode>();

    [NotMapped]
    public int DureeJours => (DateFin - DateDebut).Days;

    [NotMapped]
    public bool EstEnCours => DateTime.UtcNow >= DateDebut && DateTime.UtcNow <= DateFin;
}