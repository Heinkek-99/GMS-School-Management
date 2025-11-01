using GMS.Domain.Entities;
using GMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GMS.Infrastructure.Repositories;

public class EleveRepository : GenericRepository<Eleve>, IEleveRepository
{
    public EleveRepository(GmsDbContext context) : base(context)
    {
    }

    public async Task<Eleve?> GetByMatriculeAsync(string matricule, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(e => e.Matricule == matricule, cancellationToken);
    }

    public async Task<Eleve?> GetDossierCompletAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(e => e.Famille)
            .Include(e => e.Classe)
            .Include(e => e.AnneeScolaire)
            .Include(e => e.Frais)
                .ThenInclude(f => f.TypeFrais)
            .Include(e => e.Frais)
                .ThenInclude(f => f.Ventilations)
                    .ThenInclude(v => v.Paiement)
            .Include(e => e.Notes)
                .ThenInclude(n => n.Matiere)
            .Include(e => e.Notes)
                .ThenInclude(n => n.Periode)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Eleve>> GetByClasseAsync(Guid classeId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(e => e.Famille)
            .Where(e => e.ClasseId == classeId)
            .OrderBy(e => e.Nom)
            .ThenBy(e => e.Prenom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Eleve>> GetByFamilleAsync(Guid familleId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(e => e.Classe)
            .Include(e => e.Frais)
            .Where(e => e.FamilleId == familleId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> MatriculeExistsAsync(string matricule, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(e => e.Matricule == matricule, cancellationToken);
    }

    public async Task<string> GenerateMatriculeAsync(Guid anneeScolaireId, CancellationToken cancellationToken = default)
    {
        var anneeScolaire = await _context.AnneesScolaires
            .FirstOrDefaultAsync(a => a.Id == anneeScolaireId, cancellationToken);

        if (anneeScolaire == null)
            throw new InvalidOperationException("Année scolaire introuvable");

        var annee = anneeScolaire.Libelle.Split('-')[0];

        // Compter les élèves existants + 1
        var count = await _dbSet
            .Where(e => e.AnneeScolaireId == anneeScolaireId)
            .CountAsync(cancellationToken) + 1;

        // Format: EL{ANNEE}{NUMERO:5}
        return $"EL{annee}{count:D5}";
    }
}
