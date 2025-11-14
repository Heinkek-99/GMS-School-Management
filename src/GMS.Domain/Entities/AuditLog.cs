using System.ComponentModel.DataAnnotations.Schema;

namespace GMS.Domain.Entities;

public class AuditLog : BaseEntity
{
    public Guid UtilisateurId { get; set; }
    public Utilisateur Utilisateur { get; set; }
    public string Action { get; set; } // CREATE, UPDATE, DELETE
    public string Entite { get; set; } // Nom de la table
    public Guid EntiteId { get; set; }
    public string Description { get; set; } = string.Empty;
    // public string Resultat { get; set; } = "Success";
    public string? AnciennesValeurs { get; set; } // JSON
    public string? NouvellesValeurs { get; set; } // 
    // public bool AReussi => Resultat == "Success";
    // public string? AdresseIP { get; set; }

    /// Icône de résultat pour affichage
    // public string IconeResultat
    // {
    //     get
    //     {
    //         return Resultat switch
    //         {
    //             "Success" => "✓",
    //             "Failed" => "✗",
    //             "Warning" => "⚠",
    //             _ => "?"
    //         };
    //     }
    // }

    // /// Couleur d'affichage selon le résultat
    // public string CouleurResultat
    // {
    //     get
    //     {
    //         return Resultat switch
    //         {
    //             "Success" => "#27ae60", // Vert
    //             "Failed" => "#e74c3c", // Rouge
    //             "Warning" => "#f39c12", // Orange
    //             _ => "#95a5a6" // Gris
    //         };
    //     }
    // }

    // /// Affichage complet du log
    // public string AffichageComplet => 
    //     $"[{CreatedAt:dd/MM/yyyy HH:mm:ss}] {Utilisateur?.NomComplet ?? "Système"} - {Action} - {Entite} - {Description}";


    // /// Durée formatée

    //  public long? DureeMs { get; set; } // Durée en millisecondes
    // public string DureeFormatee
    // {
    //     get
    //     {
    //         if (!DureeMs.HasValue) return "N/A";
    //         if (DureeMs < 1000) return $"{DureeMs}ms";
    //         return $"{DureeMs / 1000.0:F2}s";
    //     }
    // }

}