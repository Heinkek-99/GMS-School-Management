public class PresenceEleveDto
{
    public Guid EleveId { get; set; }
    public StatutPresence Statut { get; set; }
    public TimeSpan? HeureArrivee { get; set; }
    public string? Justification { get; set; }
    public string? Observations { get; set; }
}