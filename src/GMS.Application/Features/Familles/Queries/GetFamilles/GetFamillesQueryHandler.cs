using System;
using System.Linq;
using GMS.Application.Features.Familles.Queries.GetFamilles;
using GMS.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GMS.Application.Features.Familles.Queries;

public class GetFamillesQueryHandler : IRequestHandler<GetFamillesQuery, List<FamilleDto>>
{
    private readonly GmsDbContext _context;

    public GetFamillesQueryHandler(GmsDbContext context)
    {
        _context = context;
    }

    public async Task<List<FamilleDto>> Handle(GetFamillesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Familles
            .Include(f => f.Eleves)
            .Include(f => f.Paiements)
            .AsQueryable();

        // Recherche
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.ToLower();
            query = query.Where(f =>
                f.NomFamille.ToLower().Contains(searchTerm) ||
                f.NomPere.ToLower().Contains(searchTerm) ||
                f.NomMere.ToLower().Contains(searchTerm) ||
                (f.TelephonePere != null && f.TelephonePere.Contains(searchTerm)) ||
                (f.TelephoneMere != null && f.TelephoneMere.Contains(searchTerm))
            );
        }

        var familles = await query
            .OrderBy(f => f.NomFamille)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(f => new FamilleDto
            {
                Id = f.Id,
                NomFamille = f.NomFamille,
                ContactPrincipal = $"{f.PrenomPere ?? ""} {f.NomPere}".Trim(),
                TelephonePrincipal = f.TelephonePere ?? f.TelephoneMere,
                EmailPrincipal = f.EmailPere ?? f.EmailMere,
                Ville = f.Ville,
                NombreEnfants = f.Eleves.Count(e => !e.IsDeleted),
                Solde = f.Eleves.Where(e => !e.IsDeleted)
                    .Sum(e => e.Frais.Where(fr => !fr.IsDeleted).Sum(fr => fr.Montant)) -
                    f.Paiements.Where(p => !p.IsDeleted).Sum(p => p.Montant),
                CreatedAt = f.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return familles;
    }
}