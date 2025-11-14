using System.ComponentModel.DataAnnotations.Schema;

namespace GMS.Domain.Entities;

public class Famille : BaseEntity
{
    public string NomFamille { get; set; }
    public string CodeFamille { get; set; }

    // Responsable
    public string NomResponsable { get; set; }
    public string? PrenomResponsable { get; set; }
    
    public string? TelephonePrincipal { get; set; }
    public string? TelephoneSecondaire { get; set; }
    public string? Email { get; set; }

    // Père
    public string NomPere { get; set; }
    public string? PrenomPere { get; set; }
    public string? TelephonePere { get; set; }
    public string? EmailPere { get; set; }

    // Mère
    public string NomMere { get; set; }
    public string? PrenomMere { get; set; }
    public string? TelephoneMere { get; set; }
    public string? EmailMere { get; set; }

    //Financier
    public decimal SoldeGlobal { get; set; }

    //Adresse
    public string Adresse { get; set; } = string.Empty;
    public string? Ville { get; set; }
    public string? CodePostal { get; set; }
    public string? Pays { get; set; } = "Cameroun";

    // Navigation
    public Guid EcoleId { get; set; }
    public virtual Ecole Ecole { get; set; } = null!;

    public ICollection<Eleve> Eleves { get; set; } = new List<Eleve>();
    public ICollection<Paiement> Paiements { get; set; } = new List<Paiement>();

    // Propriétés calculées
    public string NomComplet => $"{NomResponsable} {PrenomResponsable}";
    [NotMapped]
    public decimal TotalDu => Eleves.Sum(e => e.Frais.Sum(f => f.Montant));
    [NotMapped]
    public decimal TotalPaye => Paiements.Where(p => !p.IsDeleted).Sum(p => p.Montant);
    [NotMapped]
    public decimal Solde => TotalDu - TotalPaye;

    public string StatutFinancier => SoldeGlobal > 0 ? "À jour" : "En retard";

    [NotMapped]
    public int NombreEnfants => Eleves.Count(e => !e.IsDeleted);
    [NotMapped]
    public string LienResponsable =>
        NomResponsable == NomPere ? "Père" :
        NomResponsable == NomMere ? "Mère" : "Autre";

}