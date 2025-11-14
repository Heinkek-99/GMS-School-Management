public record FraisDto
{
    public Guid Id { get; set; }
    public string TypeFrais { get; set; } = string.Empty;
    public string Periode {get; set;} = string.Empty;
    public decimal Montant { get; set; }
    public decimal MontantPaye { get; set; }
    public decimal Solde { get; set; }
    public DateTime DateEcheance { get; set; }
    public bool EstEnRetard {get; set;}
    public string Statut { get; set; }
}