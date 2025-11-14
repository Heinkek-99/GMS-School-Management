/// <summary>
/// Statut de présence possible
/// </summary>
public enum StatutPresence
{
    /// <summary>
    /// Élève présent
    /// </summary>
    Present = 1,

    /// <summary>
    /// Élève absent sans justification
    /// </summary>
    Absent = 2,

    /// <summary>
    /// Élève en retard
    /// </summary>
    Retard = 3,

    /// <summary>
    /// Élève absent avec justification valide
    /// </summary>
    AbsentJustifie = 4,

    /// <summary>
    /// Élève excusé (convocation, maladie)
    // </summary>
    Excuse = 5

}