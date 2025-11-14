using GMS.Application.Common;
using GMS.Infrastructure.Repositories;
using MediatR;

public class SearchFamillesQueryHandler : IRequestHandler<SearchFamillesQuery, Result<List<FamilleDto>>>
{
    private readonly IFamilleRepository _familleRepository;

    public SearchFamillesQueryHandler(IFamilleRepository familleRepository)
    {
        _familleRepository = familleRepository;
    }

    public async Task<Result<List<FamilleDto>>> Handle(SearchFamillesQuery request, CancellationToken cancellationToken)
    {
        var familles = await _familleRepository.SearchAsync(request.SearchTerm, cancellationToken);

        var dtos = familles.Select(f => new FamilleDto
        {
            Id = f.Id,
            CodeFamille = f.CodeFamille,
            NomComplet = $"{f.PrenomResponsable} {f.NomResponsable}".Trim(),
            TelephonePrincipal = f.TelephonePrincipal ?? "N/A",
            NombreEnfants = f.NombreEnfants,
            SoldeGlobal = f.SoldeGlobal,
            StatutFinancier = f.StatutFinancier
        }).ToList();

        return Result<List<FamilleDto>>.Success(dtos);
    }
}