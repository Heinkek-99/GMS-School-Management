using GMS.Domain.Entities;

namespace GMS.Infrastructure.Repositories;

public interface IPaiementRepository : IGenericRepository<Paiement>
{
    Task<Paiement?> GetByNumeroAsync(string numeroPaiement, CancellationToken cancellationToken = default);
    Task<IEnumerable<Paiement>> GetByFamilleAsync(Guid familleId, CancellationToken cancellationToken = default);
    Task<string> GenerateNumeroPaiementAsync(CancellationToken cancellationToken = default);
}