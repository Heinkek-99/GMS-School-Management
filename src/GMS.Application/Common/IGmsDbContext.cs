using Microsoft.EntityFrameworkCore;
using GMS.Domain.Entities;

namespace GMS.Application.Common;

/// <summary>
/// Interface du contexte de base de données
/// </summary>
public interface IGmsDbContext
{
    DbSet<Utilisateur> Utilisateurs { get; }
    DbSet<Famille> Familles { get; }
    DbSet<Eleve> Eleves { get; }
    DbSet<AnneeScolaire> AnneesScolaires { get; }
    DbSet<Periode> Periodes { get; }
    DbSet<Classe> Classes { get; }
    DbSet<TypeFrais> TypesFrais { get; }
    DbSet<Frais> Frais { get; }
    DbSet<Paiement> Paiements { get; }
    DbSet<VentilationPaiement> VentilationsPaiements { get; }
    DbSet<Matiere> Matieres { get; }
    DbSet<Note> Notes { get; }
    DbSet<EmploiDuTemps> EmploisDuTemps { get; }
    DbSet<AuditLog> AuditLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}