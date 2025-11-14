using GMS.Application.Common;
using GMS.Domain.Entities;
using MediatR;

namespace GMS.Application.Features.Presences.Commands
{
    public class EnregistrerPresenceCommand : IRequest<Result<Guid>>
    {
        public Guid EleveId { get; set; }
        public DateTime Date { get; set; }
        public StatutPresence Statut { get; set; }
        public TimeSpan? HeureArrivee { get; set; }
        public string? Justification { get; set; }
        public string? Observations { get; set; }

    }
}