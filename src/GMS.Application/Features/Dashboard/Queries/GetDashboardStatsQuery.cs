using GMS.Application.Common;
using GMS.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GMS.Application.Features.Dashboard.Queries
{
    public class GetDashboardStatsQuery : IRequest<Result<DashboardStatsResponse>>
    {
        public Guid EcoleId { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
    }

}