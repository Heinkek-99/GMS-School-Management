using System.ComponentModel.DataAnnotations.Schema;

namespace GMS.Domain.Entities
{
    /// Représente la présence/absence d'un élève pour une journée ou un cours
    public class Presence : BaseEntity
    {
        /// Élève concerné
        public Guid EleveId { get; set; }
        public virtual Eleve Eleve { get; set; } = null!;

        /// Date de la présence
        public DateTime Date { get; set; }

        /// Statut de présence
        public StatutPresence Statut { get; set; }

        /// Heure d'arrivée (pour les retards)
        public TimeSpan? HeureArrivee { get; set; }

        /// Justification pour les absences
        public string? Justification { get; set; }

        /// Document justificatif (chemin du fichier)
        public string? DocumentJustificatif { get; set; }

        /// Observations complémentaires
        public string? Observations { get; set; }

        /// Matière concernée (optionnel, pour présence par cours)
        public Guid? MatiereId { get; set; }
        public virtual Matiere? Matiere { get; set; }

        /// Utilisateur qui a saisi la présence
        public Guid? SaisiePar { get; set; }
        public virtual Utilisateur? UtilisateurSaisie { get; set; }

        /// Date et heure de saisie
        public DateTime DateSaisie { get; set; }

        public int? DureeRetardMinutes { get; set; }

        public string? EnregistrePar { get; set; }
        public string? Commentaire { get; set; }

        public bool ParentsNotifies { get; set; }
        public DateTime? DateNotificationParents { get; set; }


        [NotMapped]
        public bool EstJustifie => Statut == StatutPresence.AbsentJustifie
            || !string.IsNullOrWhiteSpace(Justification)
            || !string.IsNullOrWhiteSpace(DocumentJustificatif);

        [NotMapped]
        public bool EstAbsenceNonJustifiee => Statut == StatutPresence.Absent && !EstJustifie;

        [NotMapped]
        public string IconeStatut => Statut switch
        {
            StatutPresence.Present => "✓",
            StatutPresence.Absent => "✗",
            StatutPresence.Retard => "⏰",
            StatutPresence.AbsentJustifie => "⚕",
            _ => "?"
        };

        [NotMapped]
        public string CouleurStatut => Statut switch
        {
            StatutPresence.Present => "#27ae60",
            StatutPresence.Absent => "#e74c3c",
            StatutPresence.Retard => "#f39c12",
            StatutPresence.AbsentJustifie => "#3498db",
            _ => "#95a5a6"
        };
    
        public string AffichageComplet =>
        $"{Date:dd/MM/yyyy} - {Eleve?.NomComplet} - {Statut}" +
        (EstJustifie ? " (Justifié)" : "");

    }
}
     
    
