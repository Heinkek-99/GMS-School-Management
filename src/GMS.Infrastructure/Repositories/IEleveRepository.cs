using GMS.Domain.Entities;

namespace GMS.Infrastructure.Repositories;

public interface IEleveRepository : IGenericRepository<Eleve>
{
    Task<Eleve?> GetByMatriculeAsync(string matricule, CancellationToken cancellationToken = default);
    Task<Eleve?> GetDossierCompletAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Eleve>> GetByClasseAsync(Guid classeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Eleve>> GetByFamilleAsync(Guid familleId, CancellationToken cancellationToken = default);
    Task<bool> MatriculeExistsAsync(string matricule, CancellationToken cancellationToken = default);
    Task<string> GenerateMatriculeAsync(Guid anneeScolaireId, CancellationToken cancellationToken = default);
}