namespace GMS.Domain.Entities;

public class AuditLog : BaseEntity
{
    public string Utilisateur { get; set; }
    public string Action { get; set; } // CREATE, UPDATE, DELETE
    public string Entite { get; set; } // Nom de la table
    public Guid EntiteId { get; set; }
    public string? AnciennesValeurs { get; set; } // JSON
    public string? NouvellesValeurs { get; set; } // JSON
    public string? AdresseIP { get; set; }
}