using GMS.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

public record DeleteFamilleCommand(Guid Id) : IRequest<Unit>;

public class DeleteFamilleCommandHandler : IRequestHandler<DeleteFamilleCommand, Unit>
{
    private readonly GmsDbContext _context;

    public DeleteFamilleCommandHandler(GmsDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteFamilleCommand request, CancellationToken cancellationToken)
    {
        var famille = await _context.Familles
            .Include(f => f.Eleves)
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (famille == null)
            throw new Exception($"Famille {request.Id} introuvable");

        // Vérifier qu'il n'y a pas d'élèves actifs
        if (famille.Eleves.Any(e => e.Statut == "Actif" && !e.IsDeleted))
            throw new Exception("Impossible de supprimer une famille avec des élèves actifs");

        // Soft delete
        famille.IsDeleted = true;
        famille.UpdatedAt = DateTime.UtcNow;
        famille.UpdatedBy = "System";

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}