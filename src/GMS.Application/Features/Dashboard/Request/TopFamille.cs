public class TopFamilles
{
    public Guid FamilleId { get; set; }
    public string NomFamille { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public decimal SoldeImpaye { get; set; }
    public int NombreEnfants { get; set; }
    public DateTime? DernierPaiement { get; set; }
}