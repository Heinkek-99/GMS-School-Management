namespace GMS.Domain.Entities;

public class Frais : BaseEntity
{
    public Guid EleveId { get; set; }
    public Eleve Eleve { get; set; }

    public Guid TypeFraisId { get; set; }
    public TypeFrais TypeFrais { get; set; }

    public decimal Montant { get; set; }
    public DateTime DateEcheance { get; set; }
    public string Statut { get; set; } // Impayé, Payé, Partiel
    public decimal MontantPaye { get; set; }
    public string? Observations { get; set; }

    // Navigation
    public ICollection<VentilationPaiement> Ventilations { get; set; } = new List<VentilationPaiement>();

    // Propriétés calculées
    public decimal Solde => Montant - MontantPaye;
    public bool EstPaye => MontantPaye >= Montant;
    public bool EstPartiel => MontantPaye > 0 && MontantPaye < Montant;

}