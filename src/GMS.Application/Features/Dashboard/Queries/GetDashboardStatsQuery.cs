using GMS.Application.Common;
using MediatR;

namespace GMS.Application.Features.Dashboard.Queries;

public record GetDashboardStatsQuery : IRequest<Result<DashboardStatsDto>>;

public record DashboardStatsDto
{
    public int NombreEleves { get; init; }
    public int NombreFamilles { get; init; }
    public decimal TotalAEncaisser { get; init; }
    public decimal TotalEncaisse { get; init; }
    public decimal TauxRecouvrement { get; init; }
    public int NombreFamillesImpayees { get; init; }
    public List<TopImpayeDto> TopImpayes { get; init; }
}

public record TopImpayeDto
{
    public Guid FamilleId { get; init; }
    public string NomFamille { get; init; }
    public decimal Solde { get; init; }
    public int NombreEnfants { get; init; }
}
