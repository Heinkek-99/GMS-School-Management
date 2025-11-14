using System.ComponentModel.DataAnnotations.Schema;

namespace GMS.Domain.Entities;

public class EmploiDuTemps : BaseEntity
{
    public Guid ClasseId { get; set; }
    public Classe Classe { get; set; }

    public Guid MatiereId { get; set; }
    public Matiere Matiere { get; set; }

    public int JourSemaine { get; set; } // Lundi, Mardi, etc.
    public TimeSpan HeureDebut { get; set; }
    public TimeSpan HeureFin { get; set; }

    public string ProfesseurNom { get; set; }
    public string? Salle { get; set; }
    public string? TypeCours { get; set; }
    public string Commentaire { get; set; }

    public Guid EcoleId { get; set; }
    public Ecole Ecole { get; set; } = null!;

    [NotMapped]
    public double DureeMinutes => (HeureFin - HeureDebut).TotalMinutes;

    // ========== COULEUR AFFICHAGE ==========
    /// Couleur pour affichage dans l'emploi du temps (code hex)
    /// Si null, utilise la couleur de la matière

    public string? CouleurAffichage { get; set; }

    // ========== PROPRIÉTÉS CALCULÉES ==========


    /// Nom du jour en français
    [NotMapped]
    public string JourNom
    {
        get
        {
            return JourSemaine switch
            {
                1 => "Lundi",
                2 => "Mardi",
                3 => "Mercredi",
                4 => "Jeudi",
                5 => "Vendredi",
                6 => "Samedi",
                _ => "Inconnu"
            };
        }
    }

    /// Affichage complet du créneau
    /// Exemple: "Lundi 08:00-10:00 - Mathématiques - Salle 201"
    [NotMapped]
    public string AffichageComplet =>
        $"{JourNom} {HeureDebut:hh\\:mm}-{HeureFin:hh\\:mm} - {Matiere?.Libelle} - {Salle}";

}