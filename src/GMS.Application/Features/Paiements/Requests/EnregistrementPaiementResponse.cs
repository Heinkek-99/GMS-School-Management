public class EnregistrerPaiementResponse
{
    public Guid PaiementId { get; set; }
    public string NumeroPaiement { get; set; } = string.Empty;
    public decimal MontantTotal { get; set; }
    public DateTime DatePaiement { get; set; }
    public int NombreVentilations { get; set; }
    public decimal NouveauSoldeFamille { get; set; }
    public string RecuUrl { get; set; } = string.Empty;
}
