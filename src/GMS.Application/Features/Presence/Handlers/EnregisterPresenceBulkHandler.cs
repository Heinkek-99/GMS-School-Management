using GMS.Application.Common;
using GMS.Application.Common.Services;
using GMS.Application.Features.Presences.Commands;
using GMS.Domain.Entities;
using GMS.Infrastructure.Data;
using GMS.Shared.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GMS.Application.Features.Presences.Handlers
{
    public class EnregistrerPresencesBulkHandler : IRequestHandler<EnregistrerPresencesBulkCommand, Result<int>>
    {
        private readonly GmsDbContext _context;

        public EnregistrerPresencesBulkHandler(GmsDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(EnregistrerPresencesBulkCommand request, CancellationToken cancellationToken)
        {
            // Validation classe
            var classe = await _context.Classes
                .FirstOrDefaultAsync(c => c.Id == request.ClasseId, cancellationToken);

            if (classe == null)
                return Result<int>.Failure("Classe introuvable");

            // Validation date
            if (request.Date.Date > DateTime.Now.Date)
                return Result<int>.Failure("La date ne peut pas être dans le futur");

            // Récupérer les élèves de la classe
            var elevesIds = request.Presences.Select(p => p.EleveId).ToList();
            var eleves = await _context.Eleves
                .Where(e => elevesIds.Contains(e.Id) && e.ClasseId == request.ClasseId && e.Statut == StatutEleve.Actif)
                .ToListAsync(cancellationToken);

            if (eleves.Count != request.Presences.Count)
                return Result<int>.Failure("Certains élèves n'ont pas été trouvés ou ne sont pas actifs");

            // Récupérer les présences existantes pour cette date
            var presencesExistantes = await _context.Presences
                .Where(p => elevesIds.Contains(p.EleveId) && 
                           p.Date.Date == request.Date.Date)
                .ToListAsync(cancellationToken);

            int nbEnregistrees = 0;

            foreach (var presenceDto in request.Presences)
            {
                var presenceExistante = presencesExistantes.FirstOrDefault(p => p.EleveId == presenceDto.EleveId);

                if (presenceExistante != null)
                {
                    // Mise à jour
                    presenceExistante.Statut = presenceDto.Statut;
                    presenceExistante.HeureArrivee = presenceDto.HeureArrivee;
                    presenceExistante.Justification = presenceDto.Justification;
                    presenceExistante.Observations = presenceDto.Observations;
                    presenceExistante.DateSaisie = DateTime.Now;
                }
                else
                {
                    // Création
                    var presence = new Presence
                    {
                        Id = Guid.NewGuid(),
                        EleveId = presenceDto.EleveId,
                        Date = request.Date.Date,
                        Statut = presenceDto.Statut,
                        HeureArrivee = presenceDto.HeureArrivee,
                        Justification = presenceDto.Justification?.Trim(),
                        Observations = presenceDto.Observations?.Trim(),
                        DateSaisie = DateTime.UtcNow,
                        CreatedBy = "System"
                    };

                    _context.Presences.Add(presence);
                }

                nbEnregistrees++;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(nbEnregistrees);
        }
    }
}