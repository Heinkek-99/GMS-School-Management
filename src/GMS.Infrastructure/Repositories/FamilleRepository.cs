using GMS.Domain.Entities;
using GMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GMS.Infrastructure.Repositories;

public class FamilleRepository : GenericRepository<Famille>, IFamilleRepository
{
    public FamilleRepository(GmsDbContext context) : base(context)
    {
    }

    public async Task<Famille?> GetByIdWithElevesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(f => f.Eleves)
                .ThenInclude(e => e.Classe)
            .Include(f => f.Eleves)
                .ThenInclude(e => e.Frais)
                    .ThenInclude(fr => fr.TypeFrais)
            .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
    }

    public async Task<Famille?> GetByIdWithPaiementsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(f => f.Paiements)
                .ThenInclude(p => p.Ventilations)
                    .ThenInclude(v => v.Frais)
            .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Famille>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        searchTerm = searchTerm.ToLower().Trim();

        return await _dbSet
            .Include(f => f.Eleves)
            .Where(f =>
                f.NomFamille.ToLower().Contains(searchTerm) ||
                f.NomPere.ToLower().Contains(searchTerm) ||
                f.NomMere.ToLower().Contains(searchTerm) ||
                (f.TelephonePere != null && f.TelephonePere.Contains(searchTerm)) ||
                (f.TelephoneMere != null && f.TelephoneMere.Contains(searchTerm)))
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Famille>> GetFamillesAvecImpayesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(f => f.Eleves)
                .ThenInclude(e => e.Frais)
            .Where(f => f.Eleves.Any(e => e.Frais.Any(fr => fr.Solde > 0)))
            .OrderByDescending(f => f.Solde)
            .ToListAsync(cancellationToken);
    }
}
