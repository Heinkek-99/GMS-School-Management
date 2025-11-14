public class DashboardStatsResponse
{
    public StatistiquesGenerales General { get; set; } = new();
    public StatistiquesFinancieres Finances { get; set; } = new();
    public StatistiquesEleves Eleves { get; set; } = new();
    public List<GraphiquePaiements> GraphiqueMensuel { get; set; } = new();
    public List<TopFamilles> FamillesImpayees { get; set; } = new();
    public List<AlerteEcheance> AlertesEcheances { get; set; } = new();
}