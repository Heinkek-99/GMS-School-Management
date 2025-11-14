using GMS.Application.Common;
using MediatR;

namespace GMS.Application.Features.Familles.Queries.GetFamilles;

/// <summary>
/// Query pour récupérer la liste des familles
/// </summary>
public record GetFamillesQuery : IRequest<Result<FamilleDto>>
{
    public Guid FamilleId { get; set; }

        public GetFamillesQuery(Guid familleId)
        {
            FamilleId = familleId;
        }

    public string? SearchTerm { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 50;
}
