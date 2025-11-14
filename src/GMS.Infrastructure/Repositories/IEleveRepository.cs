using System.Linq.Expressions;
using GMS.Domain.Entities;

namespace GMS.Infrastructure.Repositories;

public interface IEleveRepository : IGenericRepository<Eleve>
{
    Task<Eleve?> GetByIdWithFullDetailsAsync(Guid id);
    Task<Eleve?> GetByMatriculeAsync(string matricule, CancellationToken cancellationToken = default);
    Task<Eleve?> GetDossierCompletAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Eleve>> GetElevesByClasseIdAsync(Guid classeId);
    Task<IEnumerable<Eleve>> GetByClasseAsync(Guid classeId, CancellationToken cancellationToken = default);
    Task<List<Eleve>> GetElevesByFamilleIdAsync(Guid familleId);
    Task<IEnumerable<Eleve>> GetByFamilleAsync(Guid familleId, CancellationToken cancellationToken = default);
    Task<bool> MatriculeExistsAsync(string matricule, CancellationToken cancellationToken = default);
    Task<string> GenerateMatriculeAsync(Guid anneeScolaireId, CancellationToken cancellationToken = default);
    Task<(List<Eleve> Eleves, int TotalCount)> SearchElevesAsync(
            Expression<Func<Eleve, bool>> predicate,
            int pageNumber,
            int pageSize);
}