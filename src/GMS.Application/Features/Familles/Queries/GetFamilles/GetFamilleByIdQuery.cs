using GMS.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GMS.Application.Features.Familles.Queries;

public record GetFamilleByIdQuery(Guid Id) : IRequest<FamilleDetailDto?>;

public record FamilleDetailDto
{
    public Guid Id { get; init; }
    public string NomFamille { get; init; } = string.Empty;
    public string NomPere { get; init; } = string.Empty;
    public string? PrenomPere { get; init; }
    public string? TelephonePere { get; init; }
    public string? EmailPere { get; init; }
    public string NomMere { get; init; } = string.Empty;
    public string? PrenomMere { get; init; }
    public string? TelephoneMere { get; init; }
    public string? EmailMere { get; init; }
    public string Adresse { get; init; } = string.Empty;
    public string? Ville { get; init; }
    public string? CodePostal { get; init; }
    public string? Pays { get; init; }
    public List<EleveSimpleDto> Eleves { get; init; } = new();
    public decimal TotalDu { get; init; }
    public decimal TotalPaye { get; init; }
    public decimal Solde { get; init; }
    public DateTime CreatedAt { get; init; }
}

public record EleveSimpleDto
{
    public Guid Id { get; init; }
    public string Matricule { get; init; } = string.Empty;
    public string NomComplet { get; init; } = string.Empty;
    public string Classe { get; init; } = string.Empty;
    public string Statut { get; init; } = string.Empty;
    public decimal Solde { get; init; }
}

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