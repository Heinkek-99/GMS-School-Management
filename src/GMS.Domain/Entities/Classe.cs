namespace GMS.Domain.Entities;

public class Classe : BaseEntity
{
    public string Nom { get; set; } // 6ème A, CM2 B, etc.
    public string Niveau { get; set; } // Primaire, Collège, Lycée
    public int Ordre { get; set; } // Pour tri
    public int? EffectifMax { get; set; }

    public Guid? EnseignantPrincipalId { get; set; }

    // Navigation
    public ICollection<Eleve> Eleves { get; set; } = new List<Eleve>();
    public ICollection<EmploiDuTemps> EmploiDuTemps { get; set; } = new List<EmploiDuTemps>();

    // Propriétés calculées
    public int EffectifActuel => Eleves.Count(e => !e.IsDeleted && e.Statut == "Actif");
    public bool EstPleine => EffectifMax.HasValue && EffectifActuel >= EffectifMax.Value;

}
