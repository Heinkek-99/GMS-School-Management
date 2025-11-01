
using GMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GMS.Infrastructure.Services;

public class MatriculeGenerator : IMatriculeGenerator
{
    private readonly GmsDbContext _context;

    public MatriculeGenerator(GmsDbContext context)
    {
        _context = context;
    }

    public async Task<string> GenerateAsync(Guid anneeScolaireId, CancellationToken cancellationToken = default)
    {
        var anneeScolaire = await _context.AnneesScolaires
            .FirstOrDefaultAsync(a => a.Id == anneeScolaireId, cancellationToken);

        if (anneeScolaire == null)
            throw new InvalidOperationException("Année scolaire introuvable");

        var annee = anneeScolaire.Libelle.Split('-')[0];

        // Compter les élèves de cette année scolaire
        var count = await _context.Eleves
            .Where(e => e.AnneeScolaireId == anneeScolaireId)
            .CountAsync(cancellationToken) + 1;

        // Format: EL{ANNEE}{NUMERO:5}
        // Exemple: EL202400123
        return $"EL{annee}{count:D5}";
    }
}