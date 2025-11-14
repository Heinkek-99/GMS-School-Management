using GMS.Domain.Entities;

namespace GMS.Infrastructure.Repositories;

/// <summary>
/// Repository spécialisé pour Famille avec méthodes métier
/// </summary>
public interface IFamilleRepository : IGenericRepository<Famille>
{
    Task<Famille?> GetWithElevesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Famille?> GetByCodeFamilleAsync(string codeFamille, CancellationToken cancellationToken = default);

    /// Récupère toutes les familles avec un solde impayé
    Task<IEnumerable<Famille>> GetFamillesAvecSoldeImpayeAsync(CancellationToken cancellationToken = default);

    /// Recherche des familles par nom ou téléphone
    Task<IEnumerable<Famille>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);

    /// Vérifie si un code famille existe déjà
    Task<bool> CodeFamilleExistsAsync(string codeFamille, CancellationToken cancellationToken = default);

    /// Génère un nouveau code famille unique
    Task<string> GenerateCodeFamilleAsync(Guid ecoleId, CancellationToken cancellationToken = default);

    Task<Famille?> GetByIdWithDetailsAsync(Guid id);
    Task<Famille?> GetByTelephoneAsync(string telephone);
    Task RecalculerSoldeGlobalAsync(Guid familleId);
    Task<(List<Famille> Familles, int TotalCount)> SearchFamillesAsync(
        string? searchTerm,
        int pageNumber,
        int pageSize);
}