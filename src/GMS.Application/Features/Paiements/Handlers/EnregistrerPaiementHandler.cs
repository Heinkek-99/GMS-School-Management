using GMS.Application.Common;
using GMS.Application.Features.Paiements.Commands;
using GMS.Domain.Entities;
using GMS.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GMS.Application.Features.Paiements.Handlers;

public class EnregistrerPaiementHandler : IRequestHandler<EnregistrerPaiementCommand, Result<Guid>>
{
    private readonly GmsDbContext _context;

    public EnregistrerPaiementHandler(GmsDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(EnregistrerPaiementCommand request, CancellationToken cancellationToken)
    {
        // Valider que le montant total ventilé = montant payé
        var totalVentile = request.Ventilations.Sum(v => v.MontantAffecte);
        if (totalVentile != request.Montant)
            return Result<Guid>.Failure("Le montant ventilé ne correspond pas au montant payé");

        // Générer numéro de paiement
        var compteur = await _context.Paiements.CountAsync(cancellationToken) + 1;
        var numeroPaiement = $"PAY{DateTime.Now:yyyyMMdd}{compteur:D4}";

        var paiement = new Paiement
        {
            Id = Guid.NewGuid(),
            NumeroPaiement = numeroPaiement,
            FamilleId = request.FamilleId,
            Montant = request.Montant,
            DatePaiement = request.DatePaiement,
            ModePaiement = request.ModePaiement,
            NumeroReference = request.NumeroReference,
            Observations = request.Observations
        };

        await _context.Paiements.AddAsync(paiement, cancellationToken);

        // Créer les ventilations et mettre à jour les frais
        foreach (var ventDto in request.Ventilations)
        {
            var ventilation = new VentilationPaiement
            {
                Id = Guid.NewGuid(),
                PaiementId = paiement.Id,
                FraisId = ventDto.FraisId,
                MontantAffecte = ventDto.MontantAffecte
            };

            await _context.VentilationsPaiements.AddAsync(ventilation, cancellationToken);

            // Mettre à jour le frais
            var frais = await _context.Frais.FindAsync(ventDto.FraisId);
            if (frais != null)
            {
                frais.MontantPaye += ventDto.MontantAffecte;
                frais.Statut = frais.Solde == 0 ? "Payé" :
                              frais.MontantPaye > 0 ? "Partiel" : "Impayé";
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(paiement.Id);
    }
}