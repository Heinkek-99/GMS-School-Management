using GMS.Application.Common;
using MediatR;

namespace GMS.Application.Features.Familles.Queries;

public record GetFamilleByIdQuery(Guid FamilleId) : IRequest<Result<FamilleDto>>;

public record FamilleDto
{
    public Guid Id { get; init; }
    public string NomFamille { get; init; }
    public string NomPere { get; init; }
    public string? TelephonePere { get; init; }
    public string Adresse { get; init; }
    public decimal TotalDu { get; init; }
    public decimal TotalPaye { get; init; }
    public decimal Solde { get; init; }
    public List<EleveSimpleDto> Eleves { get; init; }
}

public record EleveSimpleDto
{
    public Guid Id { get; init; }
    public string Matricule { get; init; }
    public string NomComplet { get; init; }
    public string Classe { get; init; }
    public decimal SoldeEleve { get; init; }
}