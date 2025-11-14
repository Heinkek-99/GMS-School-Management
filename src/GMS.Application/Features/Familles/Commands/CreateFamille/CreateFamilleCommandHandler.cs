using GMS.Application.Common;
using GMS.Application.Features.Familles.Commands;
using GMS.Domain.Entities;
using GMS.Infrastructure.Repositories;
using MediatR;


namespace GMS.Application.Features.Familles.Handlers;

public class CreateFamilleCommandHandler : IRequestHandler<CreateFamilleCommand, Result<Guid>>
{
    private readonly IFamilleRepository _familleRepository;

    public CreateFamilleCommandHandler(IFamilleRepository familleRepository)
    {
        _familleRepository = familleRepository;
    }

    public async Task<Result<Guid>> Handle(CreateFamilleCommand request, CancellationToken cancellationToken)
    {
        var codeFamille = await _familleRepository.GenerateCodeFamilleAsync(request.EcoleId, cancellationToken);

        var famille = new Famille
        {
            Id = Guid.NewGuid(),

            EcoleId = request.EcoleId,
            CodeFamille = codeFamille,
            NomFamille = request.NomResponsable,
            NomResponsable = request.NomResponsable,
            PrenomResponsable = request.PrenomResponsable,
            TelephonePrincipal = request.TelephonePrincipal,
            Email = request.Email,
            Adresse = request.Adresse,
            SoldeGlobal = 0,
            CreatedBy = "System" // TODO: from ICurrentUserService
        };

        await _familleRepository.AddAsync(famille, cancellationToken);

        return Result<Guid>.Success(famille.Id);
    }
}