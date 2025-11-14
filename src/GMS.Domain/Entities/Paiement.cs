namespace GMS.Domain.Entities;

public class Paiement : BaseEntity
{
    public string NumeroPaiement { get; set; } = string.Empty; // Auto-généré

    public Guid FamilleId { get; set; }
    public Famille Famille { get; set; } = null;

    public decimal Montant { get; set; }
    public DateTime DatePaiement { get; set; } = DateTime.UtcNow;
    public string ModePaiement { get; set; } = string.Empty; // Espèces, Chèque, Virement, Mobile Money
    public string? NumeroReference { get; set; } // N° chèque, transaction
    public string? Observations { get; set; }

    public Guid EcoleId { get; set; }
    public Ecole Ecole { get; set; } = null!;
    
    // Ventilation du paiement
    public ICollection<VentilationPaiement> Ventilations { get; set; } = new List<VentilationPaiement>();
}
