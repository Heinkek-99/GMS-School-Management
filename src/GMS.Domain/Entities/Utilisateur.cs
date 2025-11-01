namespace GMS.Domain.Entities;

public class Utilisateur : BaseEntity
{
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } // Admin, Directeur, Secretaire, Comptable
    public bool EstActif { get; set; }
    public DateTime? DerniereConnexion { get; set; }

    // Propriétés calculées
    public string NomComplet => $"{Prenom} {Nom}";
}