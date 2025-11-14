using System.Linq.Expressions;
using GMS.Domain.Entities;

namespace GMS.Infrastructure.Repositories;

/// <summary>
/// Interface générique pour les opérations CRUD
/// </summary>
public interface IGenericRepository<T> where T : BaseEntity
{
    // Récupère une entité par son identifiant
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    // Récupère toutes les entités
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    
    // Ajoute une nouvelle entité
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    
    // Met à jour une entité existante
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    
    // Supprime une entité par son ID
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    
    // Vérifie si une entité existe par son ID
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    
    // Compte le nombre total d'entités
    Task<int> CountAsync(CancellationToken cancellationToken = default);

    /// Récupère les entités selon un prédicat
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// Récupère une seule entité selon un prédicat
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// Vérifie si une entité existe selon un prédicat
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// Compte les entités selon un prédicat
    Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    // Ajoute plusieurs entités
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// Supprime une entité (soft delete)
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    /// Supprime définitivement une entité (hard delete)
    Task HardDeleteAsync(T entity, CancellationToken cancellationToken = default);

}