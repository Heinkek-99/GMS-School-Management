using GMS.Application.Features.Familles.Commands.UpdateFamille;
using GMS.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class UpdateFamilleCommandHandler : IRequestHandler<UpdateFamilleCommand, Unit>
{
    private readonly GmsDbContext _context;

    public UpdateFamilleCommandHandler(GmsDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateFamilleCommand request, CancellationToken cancellationToken)
    {
        var famille = await _context.Familles
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (famille == null)
            throw new Exception($"Famille {request.Id} introuvable");

        famille.NomFamille = request.NomFamille;
        famille.NomPere = request.NomPere;
        famille.PrenomPere = request.PrenomPere;
        famille.TelephonePere = request.TelephonePere;
        famille.EmailPere = request.EmailPere;
        famille.NomMere = request.NomMere;
        famille.PrenomMere = request.PrenomMere;
        famille.TelephoneMere = request.TelephoneMere;
        famille.EmailMere = request.EmailMere;
        famille.Adresse = request.Adresse;
        famille.Ville = request.Ville;
        famille.CodePostal = request.CodePostal;
        famille.UpdatedAt = DateTime.UtcNow;
        famille.UpdatedBy = "System"; // TODO: User connecté

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}