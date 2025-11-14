using MediatR;  
using GMS.Application.Common;
using GMS.Infrastructure.Repositories;
using GMS.Domain.Entities;

namespace GMS.Application.Features.Familles.Handlers;

public class DeleteFamilleCommandHandler : IRequestHandler<DeleteFamilleCommand, Result<bool>>
{
    private readonly IFamilleRepository _familleRepository;
    private readonly IEleveRepository _eleveRepository;

    public DeleteFamilleCommandHandler(IFamilleRepository familleRepository, IEleveRepository eleveRepository)
    {
        _familleRepository = familleRepository;
        _eleveRepository = eleveRepository;

    }

    public async Task<Result<bool>> Handle(DeleteFamilleCommand request, CancellationToken cancellationToken)
    {
        // Récupérer la famille
        var famille = await _familleRepository.GetByIdAsync(request.Id);
        if (famille == null)
        {
            return Result<bool>.Failure($"Famille avec l'ID {request.Id} introuvable");
        }

        var familles = await _familleRepository.GetWithElevesAsync(request.Id, cancellationToken);
        if (familles == null)
            return Result<bool>.Failure("Famille introuvable");

        // Vérifier qu'il n'y a pas d'élèves actifs
        if (famille.Eleves.Any(e => !e.IsDeleted && e.Statut == "Actif"))
            return Result<bool>.Failure("Impossible de supprimer une famille avec des élèves actifs");
        

        // Vérifier le solde
        if (famille.SoldeGlobal < 0)
            return Result<bool>.Failure($"Impossible de supprimer la famille. Solde impayé de {Math.Abs(famille.SoldeGlobal):N0} FCFA. " +
                    "Veuillez régulariser les paiements avant suppression."
                );

        await _familleRepository.DeleteAsync(famille, cancellationToken);

        return Result<bool>.Success(true);
    }
}