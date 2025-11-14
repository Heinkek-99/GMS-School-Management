using GMS.Application.Common;
using GMS.Application.Features.Presences.Commands;
using GMS.Domain.Entities;
using GMS.Infrastructure.Repositories;
using MediatR;


namespace GMS.Application.Features.Presences.Handlers
{
    public class EnregistrerPresenceHandler : IRequestHandler<EnregistrerPresenceCommand, Result<Guid>>
    {
        private readonly IGenericRepository<Presence> _presenceRepository;

        public EnregistrerPresenceHandler(IGenericRepository<Presence> presenceRepository)
        {
            _presenceRepository = presenceRepository;
        }

        public async Task<Result<Guid>> Handle(EnregistrerPresenceCommand request, CancellationToken cancellationToken)
        {
            // Validation 1: Vérifier que l'élève existe et appartient à l'école
            var eleve = await _presenceRepository
                .FirstOrDefaultAsync(
                    e => e.EleveId == request.EleveId && e.Date.Date == request.Date.Date, cancellationToken);

            if (eleve == null)
                return Result<Guid>.Failure("Élève introuvable");

            // if (eleve.Statut != StatutEleve.Actif)
            //     return Result<Guid>.Failure($"L'élève n'est plus actif (statut: {eleve.Statut})");

            // Validation 2: Vérifier que la date n'est pas dans le futur
            if (request.Date.Date > DateTime.Now.Date)
                return Result<Guid>.Failure("La date de présence ne peut pas être dans le futur");

            // Validation 3: Vérifier doublon (même élève, même date, même matière)
            var existingPresence = await _presenceRepository
                .FirstOrDefaultAsync(p =>
                    p.EleveId == request.EleveId &&
                    p.Date.Date == request.Date.Date ,
                    cancellationToken);

            if (existingPresence != null)
            {
                // Mettre à jour la présence existante
                existingPresence.Statut = request.Statut;
                existingPresence.HeureArrivee = request.HeureArrivee;
                existingPresence.Justification = request.Justification;
                existingPresence.Observations = request.Observations;
                existingPresence.DateSaisie = DateTime.Now;

                await _presenceRepository.UpdateAsync(existingPresence, cancellationToken);
                return Result<Guid>.Success(existingPresence.Id);
            }

            // Validation 4: Statut obligatoire
            if (!Enum.IsDefined(typeof(StatutPresence), request.Statut))
                return Result<Guid>.Failure("Le statut de présence est obligatoire");

            // Validation 5: Pour les retards, l'heure d'arrivée est obligatoire
            if (request.Statut == StatutPresence.Retard && !request.HeureArrivee.HasValue)
                return Result<Guid>.Failure("L'heure d'arrivée est obligatoire pour un retard");

            // Validation 6: Pour les absences justifiées, la justification est obligatoire
            if ((request.Statut == StatutPresence.AbsentJustifie || request.Statut == StatutPresence.Excuse) 
                && string.IsNullOrWhiteSpace(request.Justification))
                return Result<Guid>.Failure("La justification est obligatoire pour une absence justifiée ou excuse");

            // Créer la présence
            var presence = new Presence
            {
                Id = Guid.NewGuid(),
                EleveId = request.EleveId,
                Date = request.Date.Date, // Ne garder que la date
                Statut = request.Statut,
                HeureArrivee = request.HeureArrivee,
                Justification = request.Justification?.Trim(),
                Observations = request.Observations?.Trim(),
                DateSaisie = DateTime.Now,
                CreatedBy = "System"
            };

            await _presenceRepository.AddAsync(presence, cancellationToken);

            return Result<Guid>.Success(presence.Id);
        }
    }
}