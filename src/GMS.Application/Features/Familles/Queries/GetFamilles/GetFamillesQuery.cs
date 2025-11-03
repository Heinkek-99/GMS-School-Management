using MediatR;

namespace GMS.Application.Features.Familles.Queries.GetFamilles;

/// <summary>
/// Query pour récupérer la liste des familles
/// </summary>
public record GetFamillesQuery : IRequest<List<FamilleDto>>
{
    public string? SearchTerm { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 50;
}

/// <summary>
/// DTO de retour pour une famille (liste)
/// </summary>
public record FamilleDto
{
    public Guid Id { get; init; }
    public string NomFamille { get; init; } = string.Empty;
    public string ContactPrincipal { get; init; } = string.Empty;
    public string? TelephonePrincipal { get; init; }
    public string? EmailPrincipal { get; init; }
    public string? Ville { get; init; }
    public int NombreEnfants { get; init; }
    public decimal Solde { get; init; }
    public DateTime CreatedAt { get; init; }
}