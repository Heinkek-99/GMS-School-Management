using GMS.Application.Common;
using GMS.Application.Features.Familles.Queries;
using GMS.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GMS.Application.Features.Familles.Handlers;

public class GetFamilleByIdQueryHandler : IRequestHandler<GetFamilleByIdQuery, FamilleDetailDto?>
{
    private readonly GmsDbContext _context;

    public GetFamilleByIdQueryHandler(GmsDbContext context)
    {
        _context = context;
    }

    public async Task<FamilleDetailDto?> Handle(GetFamilleByIdQuery request, CancellationToken cancellationToken)
    {
        var famille = await _context.Familles
            .Include(f => f.Eleves.Where(e => !e.IsDeleted))
                .ThenInclude(e => e.Classe)
            .Include(f => f.Eleves)
                .ThenInclude(e => e.Frais.Where(fr => !fr.IsDeleted))
            .Include(f => f.Paiements.Where(p => !p.IsDeleted))
            .Where(f => f.Id == request.Id)
            .Select(f => new FamilleDetailDto
            {
                Id = f.Id,
                NomFamille = f.NomFamille,
                NomPere = f.NomPere,
                PrenomPere = f.PrenomPere,
                TelephonePere = f.TelephonePere,
                EmailPere = f.EmailPere,
                NomMere = f.NomMere,
                PrenomMere = f.PrenomMere,
                TelephoneMere = f.TelephoneMere,
                EmailMere = f.EmailMere,
                Adresse = f.Adresse,
                Ville = f.Ville,
                CodePostal = f.CodePostal,
                Pays = f.Pays,
                Eleves = f.Eleves.Select(e => new EleveSimpleDto
                {
                    Id = e.Id,
                    Matricule = e.Matricule,
                    NomComplet = $"{e.Nom} {e.Prenom}",
                    Classe = e.Classe.Nom,
                    Statut = e.Statut,
                    Solde = e.Frais.Sum(fr => fr.Montant - fr.MontantPaye)
                }).ToList(),
                TotalDu = f.Eleves.Sum(e => e.Frais.Sum(fr => fr.Montant)),
                TotalPaye = f.Paiements.Sum(p => p.Montant),
                Solde = f.Eleves.Sum(e => e.Frais.Sum(fr => fr.Montant)) - f.Paiements.Sum(p => p.Montant),
                CreatedAt = f.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        return famille;
    }


}