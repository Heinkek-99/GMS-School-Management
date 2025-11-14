  public class EleveDossierResponse
{
    public InfosPersonnelles Eleve { get; set; } = new();
    public InfosFamille Famille { get; set; } = new();
    public InfosScolarite Scolarite { get; set; } = new();
    public BulletinFinancier Finances { get; set; } = new();
    public List<NoteDto> DernieresNotes { get; set; } = new();
}