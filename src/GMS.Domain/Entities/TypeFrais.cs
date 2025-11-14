namespace GMS.Domain.Entities;

public class TypeFrais : BaseEntity
{
    public string Code { get; set; } = string.Empty; // SCOL, INSC, CANT
    public string Libelle { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool EstRecurrent { get; set; } = false;
    public string Frequence { get; set; } = "Annuel"; // Annuel, Mensuel, Trimestriel, Ponctuel
    public decimal? MontantParDefaut { get; set; }

    public Guid EcoleId { get; set; }
    public Ecole Ecole { get; set; } = null!;
    // Navigation
    public ICollection<Frais> Frais { get; set; } = new List<Frais>();

}