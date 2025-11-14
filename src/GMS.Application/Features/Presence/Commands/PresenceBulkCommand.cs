using GMS.Application.Common;
using MediatR;

namespace GMS.Application.Features.Presences.Commands
{
    public class EnregistrerPresencesBulkCommand : IRequest<Result<int>>
    {
        public Guid EcolelId { get; set; }
        public Guid ClasseId { get; set; }
        public DateTime Date { get; set; }
        public List<PresenceEleveDto> Presences { get; set; } = new List<PresenceEleveDto>();
    }
}
