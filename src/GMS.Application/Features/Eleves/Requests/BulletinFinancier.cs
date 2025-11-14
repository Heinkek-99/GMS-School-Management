public class BulletinFinancier
{
    public decimal SoldeEleve { get; set; }
    public decimal TotalFrais { get; set; }
    public decimal TotalPaye { get; set; }
    public decimal SoldeRestant { get; set; }
    public int NombreFraisImpaye { get; set; }
    public DateTime? ProchaineEcheance { get; set; }
    public List<FraisDto> Frais { get; set; } = new();
    public List<PaiementDto> DerniersPaiements { get; set; } = new();
}