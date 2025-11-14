public class UpdateFamilleResponse
{
    public Guid Id { get; set; }
    public string NomComplet { get; set; } = string.Empty;
    public string TelephonePrincipal { get; set; } = string.Empty;
    
    public decimal SoldeGlobal { get; set; }
    public DateTime UpdatedAt { get; set; }
}