using GMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GMS.Infrastructure.Services;

public class NumeroPaiementGenerator : INumeroPaiementGenerator
{
    private readonly GmsDbContext _context;

    public NumeroPaiementGenerator(GmsDbContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateAsync(CancellationToken cancellationToken = default)
    {
        var today = DateTime.UtcNow;
        var dateStr = today.ToString("yyyyMMdd");

        // Compter les paiements du jour
        var countToday = await _context.Paiements
            .Where(p => p.DatePaiement.Date == today.Date)
            .CountAsync(cancellationToken) + 1;

        // Format: PAY{YYYYMMDD}{NUMERO:4}
        // Exemple: PAY202501220001
        return $"PAY{dateStr}{countToday:D4}";
    }
}