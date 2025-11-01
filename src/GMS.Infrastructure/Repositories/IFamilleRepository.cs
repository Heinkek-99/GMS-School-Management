using GMS.Domain.Entities;

namespace GMS.Infrastructure.Repositories;

/// <summary>
/// Repository spécialisé pour Famille avec méthodes métier
/// </summary>
public interface IFamilleRepository : IGenericRepository<Famille>
{
    Task<Famille?> GetByIdWithElevesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Famille?> GetByIdWithPaiementsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Famille>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<IEnumerable<Famille>> GetFamillesAvecImpayesAsync(CancellationToken cancellationToken = default);
}