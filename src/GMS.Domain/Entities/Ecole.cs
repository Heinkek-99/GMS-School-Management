using GMS.Domain.Entities.Enum;

namespace GMS.Domain.Entities;

/// <summary>
/// Représente un établissement scolaire
/// </summary>
public class Ecole : BaseEntity
{
    public string Nom { get; set; } = string.Empty;
    public string CodeEtablissement { get; set; } = string.Empty;   
    public string? Sigle { get; set; }
    public string? Slogan { get; set; }
    public string? LogoPath { get; set; }
    
    
    // Contact
    public string? Telephone { get; set; }
    public string? Email { get; set; }
    public string? SiteWeb { get; set; }
    public TypeEtablissement Type { get; set; }
        
    
    // Adresse
    public string Adresse { get; set; } = string.Empty;
    public string? Ville { get; set; }
    public string? CodePostal { get; set; }
    public string? Pays { get; set; } = "Cameroun";
    
    // Informations légales
    public string? NumeroAgrement { get; set; }
    public DateTime? DateCreation { get; set; }
    public string? DirecteurNom { get; set; }
    
    // Configuration
    public bool EstActif { get; set; } = true;
    public string? Devise { get; set; } = "FCFA";

    // Navigation
    public ICollection<Utilisateur> Utilisateurs { get; set; } = new List<Utilisateur>();
    public ICollection<Famille> Familles { get; set; } = new List<Famille>();
    public ICollection<Eleve> Eleves { get; set; } = new List<Eleve>();
    public ICollection<Classe> Classes { get; set; } = new List<Classe>();
    public ICollection<AnneeScolaire> AnneesScolaires { get; set; } = new List<AnneeScolaire>();
    public ICollection<TypeFrais> TypesFrais { get; set; } = new List<TypeFrais>();
    
    // Propriétés calculées
    public int NombreEleves => Eleves.Count(e => !e.IsDeleted && e.Statut == "Actif");
    public int NombreClasses => Classes.Count(c => !c.IsDeleted);
}