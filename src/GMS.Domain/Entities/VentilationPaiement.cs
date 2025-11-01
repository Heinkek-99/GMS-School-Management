namespace GMS.Domain.Entities;

public class VentilationPaiement : BaseEntity
{
    public Guid PaiementId { get; set; }
    public Paiement Paiement { get; set; } = null;

    public Guid FraisId { get; set; }
    public Frais Frais { get; set; }

    public decimal MontantAffecte { get; set; }
}