using System.Runtime.CompilerServices;

public class FamilleDto
{
    public Guid Id { get; set; }
    public string CodeFamille { get; set; } = string.Empty;
    public string NomFamille {get; set;} = string.Empty;
    public string ContactPrincipal{get; set;}
    public string NomComplet {get; set;}  = string.Empty;
    public string NomResponsable { get; set; } = string.Empty;
    public string PrenomResponsable {get; set;} = string.Empty;
    public string TelephonePrincipal { get; set; } = string.Empty;
    public string TelephoneSecondaire { get; set; } = string.Empty;
    public string NomPere {get; set;} = string.Empty;
    public string PrenomPere {get; set;} = string.Empty;
    public string TelephonePere{get; set;} = string.Empty;
    public string EmailPere {get; set;}
    public string NomMere{get; set;} = string.Empty;
    public string PrenomMere{get; set;} = string.Empty;
    public string TelephoneMere{get; set;} = string.Empty;
    public string EmailMere {get; set;}

    public string? Email { get; set; }
    public string? Adresse { get; set; }
    public string? Ville { get; set; }
    public decimal TotalDu { get; set; }
    public int NombreEnfants { get; set; }
    public decimal SoldeGlobal { get; set; }
    public decimal Solde{get; set;}
    public string Pays {get; set;} = "Cameroun";
    public string StatutFinancier { get; set; } = string.Empty;
    public List<EleveDossierResponse> Enfants { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}