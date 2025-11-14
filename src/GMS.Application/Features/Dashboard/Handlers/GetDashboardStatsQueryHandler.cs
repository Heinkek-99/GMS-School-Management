using GMS.Application.Common;
using GMS.Application.Features.Dashboard.Queries;
using GMS.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace GMS.Application.Features.Dashboard.Handlers;

public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, Result<DashboardStatsResponse>>
{
    private readonly GmsDbContext _context;

    public GetDashboardStatsQueryHandler(GmsDbContext context)
    {
        _context = context; 
    }

    public async Task<Result<DashboardStatsResponse>> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        var dateDebut = request.DateDebut ?? DateTime.Today.AddMonths(-6);
        var dateFin = request.DateFin ?? DateTime.Today;

        // 1. Statistiques générales
        var general = new StatistiquesGenerales
        {
            NombreFamilles = await _context.Familles
                .Where(f => f.EcoleId == request.EcoleId && !f.IsDeleted)
                .CountAsync(cancellationToken),
            
            NombreElevesActifs = await _context.Eleves
                .Where(e => e.EcoleId == request.EcoleId && !e.IsDeleted && e.Statut == "Actif")
                .CountAsync(cancellationToken),
            
            NombreClasses = await _context.Classes
                .Where(c => c.EcoleId == request.EcoleId && c.AnneeScolaire.EstActive)
                .CountAsync(cancellationToken),
            
            NombreUtilisateurs = await _context.Utilisateurs
                .Where(u => u.EcoleId == request.EcoleId && u.EstActif && !u.IsDeleted)
                .CountAsync(cancellationToken)
        };

        // 2. Statistiques financières
        var totalFrais = await _context.Frais
            .Where(f => f.Eleve.EcoleId == request.EcoleId)
            .SumAsync(f => f.Montant, cancellationToken);

        var totalPaye = await _context.Frais
            .Where(f => f.Eleve.EcoleId == request.EcoleId)
            .SumAsync(f => f.MontantPaye, cancellationToken);

        var paiementsAujourdhui = await _context.Paiements
            .Where(p => p.Famille.EcoleId == request.EcoleId &&
                        p.DatePaiement.Date == DateTime.Today)
            .ToListAsync(cancellationToken);

        var fraisEnRetard = await _context.Frais
            .Where(f => f.Eleve.EcoleId == request.EcoleId &&
                        f.DateEcheance < DateTime.Today &&
                        f.Statut != "Paye")
            .CountAsync(cancellationToken);

        var finances = new StatistiquesFinancieres
        {
            TotalFraisGeneres = totalFrais,
            TotalEncaisse = totalPaye,
            TotalImpaye = totalFrais - totalPaye,
            TauxRecouvrement = totalFrais > 0 ? (totalPaye / totalFrais) * 100 : 0,
            NombrePaiementsAujourdhui = paiementsAujourdhui.Count,
            MontantPaiementsAujourdhui = paiementsAujourdhui.Sum(p => p.Montant),
            NombreFraisImpaye = await _context.Frais
                .Where(f => f.Eleve.EcoleId == request.EcoleId && f.Statut == "Impaye")
                .CountAsync(cancellationToken),
            NombreFraisEnRetard = fraisEnRetard
        };

        // 3. Statistiques élèves
        var eleves = await _context.Eleves
            .Where(e => e.EcoleId == request.EcoleId && !e.IsDeleted)
            .Include(e => e.Classe)
            .ToListAsync(cancellationToken);

        var nouvellesInscriptions = eleves
            .Count(e => e.DateInscription.Month == DateTime.Today.Month &&
                        e.DateInscription.Year == DateTime.Today.Year);

        var elevesStats = new StatistiquesEleves
        {
            TotalEleves = eleves.Count,
            ElevesActifs = eleves.Count(e => e.Statut == "Actif"),
            ElevesInactifs = eleves.Count(e => e.Statut != "Actif"),
            NouvellesInscriptionsMois = nouvellesInscriptions,
            RepartitionParNiveau = eleves
                .GroupBy(e => e.Classe?.Niveau ?? "Non assigné")
                .ToDictionary(g => g.Key, g => g.Count()),
            RepartitionParSexe = eleves
                .GroupBy(e => e.Sexe == "M" ? "Masculin" : "Féminin")
                .ToDictionary(g => g.Key, g => g.Count())
        };

        // 4. Graphique paiements mensuels (6 derniers mois)
        var paiementsMensuels = await _context.Paiements
            .Where(p => p.Famille.EcoleId == request.EcoleId &&
                        p.DatePaiement >= dateDebut &&
                        p.DatePaiement <= dateFin)
            .ToListAsync(cancellationToken);

        var graphique = paiementsMensuels
            .GroupBy(p => new { p.DatePaiement.Year, p.DatePaiement.Month })
            .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
            .Select(g => new GraphiquePaiements
            {
                Mois = $"{g.Key.Month:00}/{g.Key.Year}",
                MontantEncaisse = g.Sum(p => p.Montant),
                NombrePaiements = g.Count()
            })
            .ToList();

        // 5. Top familles impayées
        var topFamillesImpayees = await _context.Familles
            .Where(f => f.EcoleId == request.EcoleId && !f.IsDeleted && f.SoldeGlobal < 0)
            .OrderBy(f => f.SoldeGlobal)
            .Take(10)
            .Select(f => new TopFamilles
            {
                FamilleId = f.Id,
                NomFamille = $"{f.NomResponsable} {f.PrenomResponsable}",
                Telephone = f.TelephonePrincipal,
                SoldeImpaye = Math.Abs(f.SoldeGlobal),
                NombreEnfants = f.Eleves.Count(e => !e.IsDeleted),
                DernierPaiement = f.Paiements.OrderByDescending(p => p.DatePaiement).FirstOrDefault()!.DatePaiement
            })
            .ToListAsync(cancellationToken);

        // 6. Alertes échéances
        var fraisProchesEcheances = await _context.Frais
            .Where(f => f.Eleve.EcoleId == request.EcoleId &&
                        f.Statut != "Paye" &&
                        f.DateEcheance >= DateTime.Today &&
                        f.DateEcheance <= DateTime.Today.AddDays(30))
            .Include(f => f.Eleve)
            .Include(f => f.TypeFrais)
            .OrderBy(f => f.DateEcheance)
            .Take(20)
            .ToListAsync(cancellationToken);

        var alertes = fraisProchesEcheances.Select(f =>
        {
            var joursRestants = (f.DateEcheance.Date - DateTime.Today).Days;
            string priorite = joursRestants <= 7 ? "Urgente" :
                                joursRestants <= 15 ? "Haute" : "Normale";

            return new AlerteEcheance
            {
                FraisId = f.Id,
                EleveNom = $"{f.Eleve.Nom} {f.Eleve.Prenom}",
                TypeFrais = f.TypeFrais.Libelle,
                Montant = f.Solde,
                DateEcheance = f.DateEcheance,
                JoursRestants = joursRestants,
                Priorite = priorite
            };
        }).ToList();

        // 7. Construire la réponse complète
        return Result<DashboardStatsResponse>.Success(new DashboardStatsResponse
        {
            General = general,
            Finances = finances,
            Eleves = elevesStats,
            GraphiqueMensuel = graphique,
            FamillesImpayees = topFamillesImpayees,
            AlertesEcheances = alertes
        });
    }
}