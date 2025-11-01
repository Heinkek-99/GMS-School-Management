namespace GMS.Domain.Entities;

public class Famille : BaseEntity
{
    public string NomFamille { get; set; }

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

    //Adresse
    public string Adresse { get; set; }
    public string? Ville { get; set; }
    public string? CodePostal { get; set; }
    public string? Pays { get; set; } = "Cameroun";

    // Navigation
    public ICollection<Eleve> Eleves { get; set; } = new List<Eleve>();
    public ICollection<Paiement> Paiements { get; set; } = new List<Paiement>();

    // Propriétés calculées
    public decimal TotalDu => Eleves.Sum(e => e.Frais.Sum(f => f.Montant));
    public decimal TotalPaye => Paiements.Where(p => !p.IsDeleted).Sum(p => p.Montant);
    public decimal Solde => TotalDu - TotalPaye;
    public int NombreEnfants => Eleves.Count(e => !e.IsDeleted);

}