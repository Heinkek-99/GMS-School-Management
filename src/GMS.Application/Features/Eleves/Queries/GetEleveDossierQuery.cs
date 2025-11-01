using GMS.Application.Common;
using MediatR;

namespace GMS.Application.Features.Eleves.Queries;

public record GetEleveDossierQuery(Guid EleveId) : IRequest<Result<DossierEleveDto>>;

public record DossierEleveDto
{
    public Guid Id { get; init; }
    public string Matricule { get; init; }
    public string NomComplet { get; init; }
    public DateTime DateNaissance { get; init; }
    public string Sexe { get; init; }
    public string Classe { get; init; }
    public string PhotoPath { get; init; }
    public string NomFamille { get; init; }
    public string TelephoneParent { get; init; }

    // Finances
    public decimal TotalFrais { get; init; }
    public decimal TotalPaye { get; init; }
    public decimal Solde { get; init; }
    public List<FraisDto> Frais { get; init; }

    // Notes
    public List<NoteDto> Notes { get; init; }
}

public record FraisDto
{
    public Guid Id { get; init; }
    public string TypeFrais { get; init; }
    public decimal Montant { get; init; }
    public decimal MontantPaye { get; init; }
    public decimal Solde { get; init; }
    public DateTime DateEcheance { get; init; }
    public string Statut { get; init; }
}

public record NoteDto
{
    public string Matiere { get; init; }
    public string Periode { get; init; }
    public decimal Note { get; init; }
    public decimal NoteSur { get; init; }
}