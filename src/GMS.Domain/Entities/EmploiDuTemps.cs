namespace GMS.Domain.Entities;

public class EmploiDuTemps : BaseEntity
{
    public Guid ClasseId { get; set; }
    public Classe Classe { get; set; }

    public Guid MatiereId { get; set; }
    public Matiere Matiere { get; set; }

    public string JourSemaine { get; set; } // Lundi, Mardi, etc.
    public TimeSpan HeureDebut { get; set; }
    public TimeSpan HeureFin { get; set; }
    public string? Salle { get; set; }
}