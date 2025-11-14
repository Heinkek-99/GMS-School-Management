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
        return await _context.Familles
            .Include(f => f.Eleves.Where(e => !e.IsDeleted))
                .ThenInclude(e => e.Classe)
            .Include(f => f.Eleves)
                .ThenInclude(e => e.Frais)
                    .ThenInclude(f => f.TypeFrais)
            .Include(f => f.Eleves)
                .ThenInclude(e => e.Frais)
                    .ThenInclude(f => f.Periode)
            .Include(f => f.Eleves)
                .ThenInclude(e => e.Frais)
                    .ThenInclude(f => f.Ventilations)
                        .ThenInclude(v => v.Paiement)
            .Include(f => f.Paiements.OrderByDescending(p => p.DatePaiement).Take(10))
            .Include(f => f.Ecole)
            .FirstOrDefaultAsync(f => f.Id == id && !f.IsDeleted);   
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
    /// <summary>
    /// Récupère une famille avec tous ses détails (élèves, paiements, etc.)
    /// </summary>
    public async Task<Famille?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.Familles
            .Include(f => f.Eleves.Where(e => !e.IsDeleted))
                .ThenInclude(e => e.Classe)
            .Include(f => f.Eleves)
                .ThenInclude(e => e.Frais)
                    .ThenInclude(f => f.TypeFrais)
            .Include(f => f.Eleves)
                .ThenInclude(e => e.Frais)
                    .ThenInclude(f => f.Periode)
            .Include(f => f.Eleves)
                .ThenInclude(e => e.Frais)
                    .ThenInclude(f => f.Ventilations)
                        .ThenInclude(v => v.Paiement)
            .Include(f => f.Paiements.OrderByDescending(p => p.DatePaiement).Take(10))
            .Include(f => f.Ecole)
            .FirstOrDefaultAsync(f => f.Id == id && !f.IsDeleted);
    }

    /// <summary>
    /// Récupère une famille par son numéro de téléphone principal
    /// </summary>
    public async Task<Famille?> GetByTelephoneAsync(string telephone)
    {
        if (string.IsNullOrWhiteSpace(telephone))
            return null;

        return await _context.Familles
            .FirstOrDefaultAsync(f => f.TelephonePrincipal == telephone && !f.IsDeleted);
    }

    /// <summary>
    /// Recalcule et met à jour le solde global d'une famille
    /// </summary>
    public async Task RecalculerSoldeGlobalAsync(Guid familleId)
    {
        var famille = await _context.Familles
            .Include(f => f.Eleves)
                .ThenInclude(e => e.Frais)
            .FirstOrDefaultAsync(f => f.Id == familleId);

        if (famille == null)
            return;

        // Calculer le solde global = somme des soldes de tous les enfants
        var soldeTotal = famille.Eleves
            .Where(e => !e.IsDeleted)
            .SelectMany(e => e.Frais)
            .Sum(f => f.Montant - f.MontantPaye);

        // Solde négatif si impayé, positif si créditeur
        famille.SoldeGlobal = -soldeTotal;

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Recherche paginée de familles avec filtres
    /// </summary>
    public async Task<(List<Famille> Familles, int TotalCount)> SearchFamillesAsync(
        string? searchTerm,
        int pageNumber,
        int pageSize)
    {
        var query = _context.Familles
            .Include(f => f.Eleves.Where(e => !e.IsDeleted))
            .Include(f => f.Ecole)
            .Where(f => !f.IsDeleted);

        // Filtre par terme de recherche
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var search = searchTerm.ToLower().Trim();
            query = query.Where(f =>
                f.NomResponsable.ToLower().Contains(search) ||
                f.PrenomResponsable.ToLower().Contains(search) ||
                f.TelephonePrincipal.Contains(search) ||
                (f.Email != null && f.Email.ToLower().Contains(search))
            );
        }

        var totalCount = await query.CountAsync();

        var familles = await query
            .OrderBy(f => f.NomResponsable)
                .ThenBy(f => f.PrenomResponsable)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (familles, totalCount);
    }

    public Task<Famille?> GetWithElevesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Famille?> GetByCodeFamilleAsync(string codeFamille, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Famille>> GetFamillesAvecSoldeImpayeAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CodeFamilleExistsAsync(string codeFamille, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<string> GenerateCodeFamilleAsync(Guid ecoleId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
    
