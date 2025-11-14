public class AlerteEcheance
{
    public Guid FraisId { get; set; }
    public string EleveNom { get; set; } = string.Empty;
    public string TypeFrais { get; set; } = string.Empty;
    public decimal Montant { get; set; }
    public DateTime DateEcheance { get; set; }
    public int JoursRestants { get; set; }
    public string Priorite { get; set; } = string.Empty;
}