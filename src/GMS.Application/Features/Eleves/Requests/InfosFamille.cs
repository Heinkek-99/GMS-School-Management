  public class InfosFamille
{
    public Guid FamilleId { get; set; }
    public string NomResponsable { get; set; } = string.Empty;
    public string PrenomResponsable { get; set; } = string.Empty;
    public string TelephonePrincipal { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Adresse { get; set; }
    public int NombreEnfantsTotal { get; set; }
}