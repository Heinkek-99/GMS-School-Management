public class StatistiquesEleves
{
    public int TotalEleves { get; set; }
    public int ElevesActifs { get; set; }
    public int ElevesInactifs { get; set; }
    public int NouvellesInscriptionsMois { get; set; }
    public Dictionary<string, int> RepartitionParNiveau { get; set; } = new();
    public Dictionary<string, int> RepartitionParSexe { get; set; } = new();
}