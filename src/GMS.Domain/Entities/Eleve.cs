namespace GMS.Domain.Entities;

public class Eleve : BaseEntity
{
    public string Matricule { get; set; } // Auto-généré
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public DateTime DateNaissance { get; set; }
    public string LieuNaissance { get; set; }
    public string Sexe { get; set; } // M/F
    public string? PhotoPath { get; set; }
    public DateTime DateInscription { get; set; }
    public string Statut { get; set; } // Actif, Suspendu, Diplômé

    // Relations
    public Guid FamilleId { get; set; }
    public Famille Famille { get; set; }

    public Guid ClasseId { get; set; }
    public Classe Classe { get; set; }

    public Guid AnneeScolaireId { get; set; }
    public AnneeScolaire AnneeScolaire { get; set; }

    // Navigation
    public ICollection<Frais> Frais { get; set; } = new List<Frais>();
    public ICollection<Note> Notes { get; set; } = new List<Note>();

    // Propriétés calculées
    public string NomComplet => $"{Nom} {Prenom}";
    public int Age => DateTime.UtcNow.Year - DateNaissance.Year;
    public decimal TotalFrais => Frais.Where(f => !f.IsDeleted).Sum(f => f.Montant);
    public decimal TotalPaye => Frais.Where(f => !f.IsDeleted).Sum(f => f.MontantPaye);
    public decimal Solde => TotalFrais - TotalPaye;

}