using GMS.Application.Common;
using MediatR;

namespace GMS.Application.Features.Presences.Queries
{
    public record GetPresencesQuery(Guid EleveId, DateTime DateDebut, DateTime DateFin) 
    : IRequest<Result<List<PresenceDto>>>;
}
