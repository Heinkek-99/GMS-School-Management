namespace GMS.Domain.Entities;

public class Utilisateur : BaseEntity
{
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string PhotoPath { get; set; }
    public string Role { get; set; } // Admin, Directeur, Secretaire, Comptable
    public bool EstActif { get; set; }
    public DateTime? DerniereConnexion { get; set; }
    public Guid EcoleId { get; set; }
    public Ecole Ecole { get; set; } = null!;

    // Propriétés calculées
    public string NomComplet => $"{Prenom} {Nom}";
    
    // Navigation
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

}