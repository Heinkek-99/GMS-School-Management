public class InfosPersonnelles
{
    public Guid Id { get; set; }
    public string Matricule { get; set; } = string.Empty;
    public string NomComplet { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public DateTime DateNaissance { get; set; }
    public int Age { get; set; }
    public string LieuNaissance { get; set; } = string.Empty;
    public string Sexe { get; set; } = string.Empty;
    public string? PhotoPath { get; set; }
    public string Statut { get; set; } = string.Empty;
    // public decimal SoldeFinancier { get; init; }
    public DateTime DateInscription { get; set; }
}