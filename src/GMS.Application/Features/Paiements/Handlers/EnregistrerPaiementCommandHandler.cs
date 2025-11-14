using GMS.Application.Common;
using GMS.Application.Common.Services;
using GMS.Application.Features.Paiements.Commands;
using GMS.Domain.Entities;
using GMS.Infrastructure.Data;
using GMS.Infrastructure.Repositories;
using GMS.Infrastructure.Services;
using GMS.Shared.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GMS.Application.Features.Paiements.Handlers;

public class EnregistrerPaiementCommandHandler : IRequestHandler<EnregistrerPaiementCommand, Result<Guid>>
{
    private readonly IPaiementRepository _paiementRepository;
    private readonly IFamilleRepository _familleRepository;
    private readonly IGenericRepository<Frais> _fraisRepository;
    private readonly IGenericRepository<VentilationPaiement> _ventilationRepository;
    private readonly INumeroPaiementGenerator _numeroPaiementGenerator;
    private readonly ICurrentUserService _currentUserService;
    public EnregistrerPaiementCommandHandler(
        IPaiementRepository paiementRepository,
        IFamilleRepository familleRepository,
        IGenericRepository<Frais> fraisRepository,
        IGenericRepository<VentilationPaiement> ventilationRepository,
        INumeroPaiementGenerator numeroPaiementGenerator,
        ICurrentUserService currentUserService
        )       

    
    { _paiementRepository = paiementRepository;
        _familleRepository = familleRepository;
        _fraisRepository = fraisRepository;
        _ventilationRepository = ventilationRepository;
        _numeroPaiementGenerator = numeroPaiementGenerator;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Guid>> Handle(EnregistrerPaiementCommand request, CancellationToken cancellationToken)
    {
        // 1. Vérifier que la famille existe
        var famille = await _familleRepository.GetByIdAsync(request.FamilleId);
        if (famille == null)
        {
            return Result<Guid>.Failure("Famille introuvable");
        }

        // 

        // 


        // 2. Créer le paiement
        var paiement = new Paiement
        {
            Id = Guid.NewGuid(),
            FamilleId = request.FamilleId,
            Montant = request.Montant,
            DatePaiement = request.DatePaiement,
            ModePaiement = request.ModePaiement,
            NumeroReference = request.NumeroReference,
            Observations = request.Observations,
            CreatedBy = "System"
        };
        await _paiementRepository.AddAsync(paiement, cancellationToken);

        // 3. Créer les ventilations
        foreach (var ventilationDto in request.Ventilations)
        {
            var ventilation = new VentilationPaiement
            {
                Id = Guid.NewGuid(),
                PaiementId = paiement.Id,
                FraisId = ventilationDto.FraisId,
                MontantAffecte = ventilationDto.MontantAffecte,
                CreatedBy = "System"
            };
            await _ventilationRepository.AddAsync(ventilation, cancellationToken);
        }

        // 4. Mise à jours des frais associés
        foreach (var ventilationDto in request.Ventilations)
        {
            var frais = await _fraisRepository.GetByIdAsync(ventilationDto.FraisId, cancellationToken);
            if (frais != null)
            {
                frais.MontantPaye += ventilationDto.MontantAffecte;

                // Mettre à jour le statut
                if (frais.MontantPaye >= frais.Montant)
                {
                    frais.Statut = StatutFrais.Paye;
                }
                else if (frais.MontantPaye > 0)
                {
                    frais.Statut = StatutFrais.Partiel;
                }

                await _fraisRepository.UpdateAsync(frais, cancellationToken);
            }
        }

        // 5. Recalculer solde de la famille
        var eleves = await _familleRepository
            .FirstOrDefaultAsync(f => f.Id == request.FamilleId, cancellationToken);
        if (eleves != null)
        {
            eleves.SoldeGlobal -= request.Montant;
            await _familleRepository.UpdateAsync(eleves, cancellationToken);
        }
        else
        {
            return Result<Guid>.Failure("Erreur lors du recalcul du solde de la famille");
        }
        return Result<Guid>.Success(paiement.Id);             
    }
    
}
