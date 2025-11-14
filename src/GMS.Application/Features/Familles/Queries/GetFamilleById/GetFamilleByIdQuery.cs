using GMS.Application.Common;
using GMS.Application.Features.Familles.Queries.GetFamilles;
using MediatR;

namespace GMS.Application.Features.Familles.Queries;

public record GetFamilleByIdQuery(Guid FamilleId) : IRequest<Result<FamilleDto>>;
