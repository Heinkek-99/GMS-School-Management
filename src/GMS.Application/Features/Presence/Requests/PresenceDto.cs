public class PresenceDto
{
    public DateTime Date { get; set; }
    public string StatutPresence { get; set; } = string.Empty;
    public TimeSpan? HeureArrivee { get; set; }
    public string? Justification { get; set; }
    public bool EstJustifie { get; init; }
}