using MediatR;using GMS.Application.Common;
using GMS.Infrastructure.Repositories;
using GMS.Domain.Entities;

namespace GMS.Application.Features.Familles.Handlers;

public class UpdateFamilleCommandHandler : IRequestHandler<UpdateFamilleCommand, Result<bool>>
{
    private readonly IFamilleRepository _familleRepository;

    public UpdateFamilleCommandHandler(IFamilleRepository familleRepository)
    {
        _familleRepository = familleRepository;
    }

    public async Task<Result<bool>> Handle(UpdateFamilleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var famille = await _familleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (famille == null)
                return Result<bool>.Failure($"Famille {request.Id} introuvable");

            famille.NomResponsable = request.NomResponsable;
            famille.PrenomResponsable = request.PrenomResponsable;
            famille.TelephonePrincipal = request.TelephonePrincipal;
            famille.NomFamille = request.NomFamille;
            famille.NomPere = request.NomPere;
            famille.Email = request.Email;
            famille.Adresse = request.Adresse;
            famille.UpdatedBy = "System";
            
            await _familleRepository.UpdateAsync(famille, cancellationToken);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Erreur: {ex.Message}");
        }
    }

}