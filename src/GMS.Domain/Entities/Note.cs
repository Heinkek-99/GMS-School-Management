using System.ComponentModel.DataAnnotations.Schema;

namespace GMS.Domain.Entities;

public class Note : BaseEntity
{
    public Guid EleveId { get; set; }
    public Eleve Eleve { get; set; } = null;

    public Guid MatiereId { get; set; }
    public Matiere Matiere { get; set; } = null;

    public Guid PeriodeId { get; set; }
    public Periode Periode { get; set; } = null;

    public decimal Valeur { get; set; }
    public decimal NoteSur {get; set; } = 20; //20, 100
    public decimal NoteSur20 { get; private set; }
    public string? TypeEvaluation { get; set; } // Devoir, Composition
    public DateTime DateEvaluation { get; set; }
    public string? Intitule { get; set; }
    public string? Appreciation { get; set; }
    public string Commentaire { get; set; }
    public string? ProfesseurNom { get; set; }

    
    // Propriétés calculées
    // ========== STATISTIQUES ==========
    // Note minimale de la classe pour cette évaluation
    public decimal? NoteMinClasse { get; set; }


    /// Note maximale de la classe
    public decimal? NoteMaxClasse { get; set; }


    /// Moyenne de la classe
    public decimal? MoyenneClasse { get; set; }


    /// Rang de l'élève dans la classe pour cette évaluation
    public int? RangClasse { get; set; }

    // ========== VALIDATION ==========
    

    /// Indique si la note est validée (ne peut plus être modifiée)
    public bool EstValidee { get; set; }


    /// Date de validation
    public DateTime? DateValidation { get; set; }

    // ========== PROPRIÉTÉS CALCULÉES ==========


    /// Note sur 20 (standardisée)
    public void CalulerNoteSur()
    {
      if(NoteSur20 != 0)
        {
            NoteSur20 = (Valeur / NoteSur) * 20;
        }else
        {
            NoteSur20 = 0;
        }

    }


    /// Mention obtenue 
    [NotMapped]
    public string Mention
    {
        get
        {
            var note = NoteSur20;
            if (note >= 16) return "Très Bien";
            if (note >= 14) return "Bien";
            if (note >= 12) return "Assez Bien";
            if (note >= 10) return "Passable";
            return "Insuffisant";
        }
    }


    /// Vérifie si la note est supérieure à la moyenne de la classe
    [NotMapped]
    public bool EstAuDessusMoyenne => MoyenneClasse.HasValue && Valeur > MoyenneClasse.Value;


    /// Écart avec la moyenne de la classe
    [NotMapped]
    public decimal? EcartMoyenne => MoyenneClasse.HasValue ? Valeur - MoyenneClasse.Value : null;


    /// Affichage complet de la note
    /// Exemple: "15/20 - Mathématiques - 1er Trimestre"
    [NotMapped]
    public string AffichageComplet => $"{Valeur}/{Matiere?.NoteMax ?? 20} - {Matiere?.Libelle} - {Periode?.Libelle}";

}