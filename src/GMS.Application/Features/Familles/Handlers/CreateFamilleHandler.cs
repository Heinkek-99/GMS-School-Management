using GMS.Application.Common;
using GMS.Application.Features.Familles.Commands.CreateFamille;
using GMS.Domain.Entities;
using GMS.Infrastructure.Repositories;
using MediatR;


namespace GMS.Application.Features.Familles.Handlers;

public class CreateFamilleHandler : IRequestHandler<CreateFamilleCommand, Result<Guid>>
{
    private readonly IGenericRepository<Famille> _familleRepo;

    public CreateFamilleHandler(IGenericRepository<Famille> familleRepo)
    {
        _familleRepo = familleRepo;
    }

    public async Task<Result<Guid>> Handle(CreateFamilleCommand request, CancellationToken cancellationToken)
    {
        var famille = new Famille
        {
            Id = Guid.NewGuid(),
            NomFamille = request.NomFamille,
            NomPere = request.NomPere,
            PrenomPere = request.PrenomPere,
            TelephonePere = request.TelephonePere,
            EmailPere = request.EmailPere,
            NomMere = request.NomMere,
            PrenomMere = request.PrenomMere,
            TelephoneMere = request.TelephoneMere,
            EmailMere = request.EmailMere,
            Adresse = request.Adresse,
            Ville = request.Ville,
            CodePostal = request.CodePostal,
            Pays = request.Pays,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "System" // TODO: Récupérer user connecté
 
        };

        await _familleRepo.AddAsync(famille);
        return Result<Guid>.Success(famille.Id);
    }
}