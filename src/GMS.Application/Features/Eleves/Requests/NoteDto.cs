public record NoteDto
{
    public string Matiere { get; init; } = string.Empty;
    public string Valeur {get; set;} 
    public string Periode { get; init; } = string.Empty;
    public string Appreciation {get; set;} = string.Empty;
    public decimal Note { get; init; }
    public decimal NoteSur { get; init; }
    public DateTime Date {get; init; }
}