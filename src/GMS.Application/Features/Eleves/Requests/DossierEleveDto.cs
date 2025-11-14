public record DossierEleveDto
{
    public Guid Id { get; init; }
    public string Matricule { get; init; }
    public string NomComplet { get; init; }
    public DateTime DateNaissance { get; init; }
    public string Sexe { get; init; }
    public string Classe { get; init; }
    public string PhotoPath { get; init; }
    public string NomFamille { get; init; }
    public string TelephoneParent { get; init; }

    // Finances
    public decimal TotalFrais { get; init; }
    public decimal TotalPaye { get; init; }
    public decimal Solde { get; init; }
    public List<FraisDto> Frais { get; init; }

    // Notes
    public List<NoteDto> Notes { get; init; }
}