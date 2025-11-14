using System.ComponentModel.DataAnnotations.Schema;

namespace GMS.Domain.Entities;

public class Classe : BaseEntity
{
    public string Nom { get; set; } // 6ème A, CM2 B, etc.
    public string Niveau { get; set; } // Primaire, Collège, Lycée
    public string Section { get; set; } // A, B, C, etc.

    public string Libelle { get; set; } // Libellé complet, ex: "6ème A - Collège"
    public int Ordre { get; set; } // Pour trier les classes dans un ordre spécifique
    public int? EffectifMax { get; set; }

    public Guid? AnneeScolaireId { get; set; }
    public AnneeScolaire? AnneeScolaire { get; set; }

    public string? EnseignantPrincipal { get; set; }
    public string? TelephoneProfesseur { get; set; }

    public Guid EcoleId { get; set; }
    public Ecole Ecole { get; set; } = null!;

    // Navigation
    public ICollection<Eleve> Eleves { get; set; } = new List<Eleve>();
    public ICollection<EmploiDuTemps> EmploiDuTemps { get; set; } = new List<EmploiDuTemps>();

    // Propriétés calculées
    [NotMapped]
    public int EffectifActuel => Eleves.Count(e => !e.IsDeleted && e.Statut == "Actif");
    [NotMapped]
    public bool EstPleine => EffectifMax.HasValue && EffectifActuel >= EffectifMax.Value;
    [NotMapped]
    public int PlacesDisponibles => EffectifMax.HasValue ? Math.Max(0, EffectifMax.Value - EffectifActuel) : 0;

}
