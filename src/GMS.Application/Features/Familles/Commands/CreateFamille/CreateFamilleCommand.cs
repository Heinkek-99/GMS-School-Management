using GMS.Application.Common;
using MediatR;

namespace GMS.Application.Features.Familles.Commands;

public record CreateFamilleCommand : IRequest<Result<Guid>>
{
    public Guid EcoleId { get; init; }
    public string NomResponsable { get; init; } 
    public string PrenomResponsable { get; init; }
    public string TelephonePrincipal { get; init; }
    public string Email { get; init; }
    public string NomFamille { get; init; }
    public string NomPere { get; init; }
    public string? PrenomPere { get; init; }
    public string? TelephonePere { get; init; }
    public string? EmailPere { get; init; }
    public string NomMere { get; init; }
    public string? PrenomMere { get; init; }
    public string? TelephoneMere { get; init; }
    public string? EmailMere { get; init; }
    public string Adresse { get; init; }
    public string? Ville { get; init; }
    public string? CodePostal { get; init; }
    public string? Pays { get; init; }
}