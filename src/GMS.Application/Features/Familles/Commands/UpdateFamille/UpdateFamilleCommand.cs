using GMS.Application.Common;
using MediatR;

namespace GMS.Application.Features.Familles.Commands.UpdateFamille;

public record UpdateFamilleCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public string NomFamille { get; init; } = string.Empty;
    public string NomPere { get; init; } = string.Empty;
    public string? PrenomPere { get; init; }
    public string? TelephonePere { get; init; }
    public string? EmailPere { get; init; }
    public string NomMere { get; init; } = string.Empty;
    public string? PrenomMere { get; init; }
    public string? TelephoneMere { get; init; }
    public string? EmailMere { get; init; }
    public string Adresse { get; init; } = string.Empty;
    public string? Ville { get; init; }
    public string? CodePostal { get; init; }

}
