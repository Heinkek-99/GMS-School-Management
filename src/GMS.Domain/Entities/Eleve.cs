using System.ComponentModel.DataAnnotations.Schema;

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

    // ========== INFORMATIONS SCOLAIRES ==========

    /// Date d'inscription à l'école
    public DateTime DateInscription { get; set; }

    /// Statut actuel de l'élève
    public string Statut { get; set; } = "Actif";// Actif, Suspendu, Diplômé
    
    /// École précédente fréquentée
    public string? EcolePrecedente { get; set; }
    /// Classe précédente fréquentée
    public string? ClassePrecedente { get; set; }
    /// Raison du changement d'école
    public string? CommentaireScolaire { get; set; }


    // ========== INFORMATIONS MÉDICALES ==========

    /// Groupe sanguin (A+, O-, etc.)
    public string? GroupeSanguin { get; set; }

    /// Allergies connues
    public string? Allergies { get; set; }

    /// Problèmes de santé particuliers
    public string? ProblemeSante { get; set; }

    /// Contact médecin traitant
    public string? MedecinTraitant { get; set; }
    

    // Relations
    public Guid FamilleId { get; set; }
    public Famille Famille { get; set; }

    public Guid ClasseId { get; set; }
    public Classe Classe { get; set; }

    public Guid EcoleId { get; set; }
    public Ecole Ecole { get; set; } = null!;

    public Guid AnneeScolaireId { get; set; }
    public AnneeScolaire AnneeScolaire { get; set; }

    // Navigation
    public ICollection<Frais> Frais { get; set; } = new List<Frais>();
    public ICollection<Note> Notes { get; set; } = new List<Note>();
    public ICollection<Presence> Presences { get; set; } = new List<Presence>();
    
    public decimal SoldeFinancier { get; set; }
    
    // Propriétés calculées
    [NotMapped]
    public string NomComplet => $"{Nom} {Prenom}";
    [NotMapped]
    public int Age => DateTime.UtcNow.Year - DateNaissance.Year;
    [NotMapped]
    public decimal TotalFrais => Frais.Where(f => !f.IsDeleted).Sum(f => f.Montant);
    [NotMapped]
    public decimal TotalPaye => Frais.Where(f => !f.IsDeleted).Sum(f => f.MontantPaye);
 
    /// Statut financier de l'élève
    [NotMapped]
    public string StatutFinancier
    {
        get
        {
            if (SoldeFinancier == 0) return "À jour";
            if (SoldeFinancier > 0) return "Créditeur";
            return "Impayé";
        }
    }

    /// Vérifie si l'élève est actif
    public bool EstActif => !IsDeleted && Statut == "Actif";

    public string Nationalite { get; set; }
}