using GMS.Application.Common;
using GMS.Application.Features.Presences.Queries;
using GMS.Domain.Entities;
using GMS.Infrastructure.Repositories;
using MediatR;

public class GetPresencesQueryHandler : IRequestHandler<GetPresencesQuery, Result<List<PresenceDto>>>
{
    private readonly IGenericRepository<Presence> _presenceRepository;

    public GetPresencesQueryHandler(IGenericRepository<Presence> presenceRepository)
    {
        _presenceRepository = presenceRepository;
    }

    public async Task<Result<List<PresenceDto>>> Handle(GetPresencesQuery request, CancellationToken cancellationToken)
    {
        var presences = await _presenceRepository.GetAllAsync(
            p => p.EleveId == request.EleveId && 
                 p.Date >= request.DateDebut && 
                 p.Date <= request.DateFin,
            cancellationToken);

        var dtos = presences.Select(p => new PresenceDto
        {
            Date = p.Date,
            StatutPresence = p.Statut.ToString(),
            Justification = p.Justification,
            EstJustifie = p.EstJustifie
        }).ToList();

        return Result<List<PresenceDto>>.Success(dtos);
    }
}