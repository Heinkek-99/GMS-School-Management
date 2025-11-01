using GMS.Domain.Entities;
using GMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GMS.Infrastructure.Repositories;

public class PaiementRepository : GenericRepository<Paiement>, IPaiementRepository
{
    public PaiementRepository(GmsDbContext context) : base(context)
    {
    }

    public async Task<Paiement?> GetByNumeroAsync(string numeroPaiement, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Ventilations)
                .ThenInclude(v => v.Frais)
                    .ThenInclude(f => f.Eleve)
            .FirstOrDefaultAsync(p => p.NumeroPaiement == numeroPaiement, cancellationToken);
    }

    public async Task<IEnumerable<Paiement>> GetByFamilleAsync(Guid familleId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Ventilations)
                .ThenInclude(v => v.Frais)
                    .ThenInclude(f => f.Eleve)
            .Where(p => p.FamilleId == familleId)
            .OrderByDescending(p => p.DatePaiement)
            .ToListAsync(cancellationToken);
    }

    public async Task<string> GenerateNumeroPaiementAsync(CancellationToken cancellationToken = default)
    {
        var today = DateTime.UtcNow;
        var dateStr = today.ToString("yyyyMMdd");

        // Compter les paiements du jour
        var countToday = await _dbSet
            .Where(p => p.DatePaiement.Date == today.Date)
            .CountAsync(cancellationToken) + 1;

        // Format: PAY{YYYYMMDD}{NUMERO:4}
        return $"PAY{dateStr}{countToday:D4}";
    }
}