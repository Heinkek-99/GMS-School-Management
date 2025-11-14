using GMS.Application.Common;
using GMS.Infrastructure.Repositories;
using MediatR;

public class GetAllFamillesQueryHandler : IRequestHandler<GetAllFamillesQuery, Result<List<FamilleDto>>>
{
    private readonly IFamilleRepository _familleRepository;

    public GetAllFamillesQueryHandler(IFamilleRepository familleRepository)
    {
        _familleRepository = familleRepository;
    }

    public async Task<Result<List<FamilleDto>>> Handle(GetAllFamillesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query  = await _familleRepository.GetAllAsync();

            var familles= query.Where(f => f.EcoleId == request.EcoleId && !f.IsDeleted);

            // Filtrer par terme de recherche
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                familles = familles.Where(f =>
                    f.NomFamille.ToLower().Contains(searchTerm) ||
                    f.NomResponsable.ToLower().Contains(searchTerm) ||
                    f.TelephonePrincipal.Contains(searchTerm) ||
                    (f.Email != null && f.Email.ToLower().Contains(searchTerm))
                );
            }

            // Pagination
            var pagedFamilles = familles
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                    .ToList();

            // Mapper vers DTO
            var famillesDtos = familles.Select(f => new FamilleDto
            {
                Id = f.Id,
                CodeFamille = f.CodeFamille,
                NomFamille = f.NomFamille,
                NomComplet = f.NomComplet,
                NomResponsable = f.NomResponsable,
                PrenomResponsable = f.PrenomResponsable ?? string.Empty,
                TelephonePrincipal = f.TelephonePrincipal ?? string.Empty,
                TelephoneSecondaire = f.TelephoneSecondaire,
                Email = f.Email,
                
                
                // Père
                NomPere = f.NomPere,
                PrenomPere = f.PrenomPere,
                TelephonePere = f.TelephonePere,
                EmailPere = f.EmailPere,
                
                // Mère
                NomMere = f.NomMere,
                PrenomMere = f.PrenomMere,
                TelephoneMere = f.TelephoneMere,
                EmailMere = f.EmailMere,
                
                // Adresse
                Adresse = f.Adresse,
                Ville = f.Ville,
                
                // Financier
                SoldeGlobal = f.SoldeGlobal,
                Solde = f.SoldeGlobal, // Alias
                StatutFinancier = f.StatutFinancier,
                
                // Relations
                NombreEnfants = f.NombreEnfants,
                
                CreatedAt = f.CreatedAt
            }).ToList();

            return Result<List<FamilleDto>>.Success(famillesDtos);
        }
        catch (Exception ex)
        {
            return Result<List<FamilleDto>>.Failure($"Erreur lors de la récupération des familles: {ex.Message}");
        }
        
    }
}