public class PaiementDto
{
    public string NumeroPaiement { get; set; } = string.Empty;
    public DateTime DatePaiement { get; set; }
    public decimal MontantVentile { get; set; }
    public string ModePaiement { get; set; } = string.Empty;
}