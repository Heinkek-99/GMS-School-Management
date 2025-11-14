using GMS.Application.Common;
using GMS.Application.Features.Eleves.Commands;
using GMS.Application.Features.Eleves.Requests;
using GMS.Domain.Entities;
using GMS.Infrastructure.Repositories;
using GMS.Infrastructure.Services;
using GMS.Shared.Constants;
using MediatR;

namespace GMS.Application.Features.Eleves.Handlers;

/// <summary>
/// Handler pour la création d'un élève avec génération automatique des frais
/// </summary>
public class CreateEleveHandler : IRequestHandler<CreateEleveCommand, Result<Guid>>
{
    private readonly IEleveRepository _eleveRepository;
    private readonly IFamilleRepository _familleRepository;
    private readonly IGenericRepository<Classe> _classeRepository;
    private readonly IGenericRepository<AnneeScolaire> _anneeScolaireRepository;
    private readonly IGenericRepository<TypeFrais> _typeFraisRepository;
    private readonly IGenericRepository<Periode> _periodeRepository;
    private readonly IGenericRepository<Frais> _fraisRepository;
    private readonly IMatriculeGenerator _matriculeGenerator;
    private readonly INumeroPaiementGenerator _numeroPaiementGenerator;
    

    public CreateEleveHandler(
        IEleveRepository eleveRepository,
        IFamilleRepository familleRepository,
        IGenericRepository<Classe> classeRepository,
        IGenericRepository<AnneeScolaire> anneeScolaireRepository,
        IGenericRepository<TypeFrais> typeFraisRepository,
        IGenericRepository<Periode> periodeRepository,
        IGenericRepository<Frais> fraisRepository
        )
    {
        _eleveRepository = eleveRepository;
        _familleRepository = familleRepository;
        _classeRepository = classeRepository;
        _anneeScolaireRepository = anneeScolaireRepository;
        _typeFraisRepository = typeFraisRepository;
        _periodeRepository = periodeRepository;
        _fraisRepository = fraisRepository;
    }

    public async Task<Result<Guid>> Handle(CreateEleveCommand request, CancellationToken cancellationToken)
    {
        // 1. Vérifier que la famille existe
        var famille = await _familleRepository.GetByIdAsync(request.FamilleId);
        if (famille == null)
            return Result<Guid>.Failure("Famille introuvable");

        // 2. Vérifier que la classe existe
        var classe = await _classeRepository.GetByIdAsync(request.ClasseId);
        if (classe == null)
            return Result<Guid>.Failure("Classe introuvable");

        // 3. Vérifier la capacité de la classe
        var elevesActifs = await _eleveRepository.GetByClasseAsync(request.ClasseId, cancellationToken);
        var effectifActuel = elevesActifs.Count(e => e.Statut == StatutEleve.Actif && !e.IsDeleted);
        
        if (classe.EffectifMax.HasValue && effectifActuel >= classe.EffectifMax.Value)
        {
            return Result<Guid>.Failure(
                $"La classe {classe.Nom} est complète (capacité max: {classe.EffectifMax})"
            );
        }

        // 4. Vérifier que l'année scolaire existe
        var anneeScolaire = await _anneeScolaireRepository.GetByIdAsync(request.AnneeScolaireId);
        if (anneeScolaire == null)
            return Result<Guid>.Failure("Année scolaire introuvable");

        // 5. Générer le matricule unique
        var matricule = await _eleveRepository.GenerateMatriculeAsync(request.AnneeScolaireId, cancellationToken);

        // 6. Créer l'élève
        var eleve = new Eleve
        {
            Id = Guid.NewGuid(),
            EcoleId = famille.EcoleId,
            FamilleId = request.FamilleId,
            ClasseId = request.ClasseId,
            AnneeScolaireId = request.AnneeScolaireId,
            Matricule = matricule,
            Nom = request.Nom.Trim().ToUpper(),
            Prenom = request.Prenom.Trim(),
            DateNaissance = request.DateNaissance,
            LieuNaissance = request.LieuNaissance.Trim(),
            Sexe = request.Sexe.ToUpper(),
            Nationalite = request.Nationalite ?? "Camerounaise",
            PhotoPath = request.PhotoPath,
            DateInscription = DateTime.UtcNow,
            Statut = StatutEleve.Actif,
            SoldeFinancier = 0,
            CreatedBy = "System" // TODO: Récupérer de ICurrentUserService
        };

        await _eleveRepository.AddAsync(eleve);

        // 7. Générer automatiquement les frais standards
        await GenererFraisStandardsAsync(eleve.Id, request.AnneeScolaireId, famille.EcoleId, cancellationToken);

        // 8. Recalculer le solde global de la famille
        await RecalculerSoldeFamilleAsync(request.FamilleId, cancellationToken);

        return Result<Guid>.Success(eleve.Id);
    }

    /// <summary>
    /// Génère automatiquement les frais standards pour un nouvel élève
    /// </summary>
    private async Task GenererFraisStandardsAsync(
        Guid eleveId, 
        Guid anneeScolaireId, 
        Guid ecoleId,
        CancellationToken cancellationToken)
    {
        // Récupérer les types de frais récurrents de l'école
        var typesFraisRecurrents = await _typeFraisRepository
            .GetAllAsync(tf => tf.EcoleId == ecoleId && tf.EstRecurrent && !tf.IsDeleted);

        if (!typesFraisRecurrents.Any())
            return;

        // Récupérer toutes les périodes de l'année scolaire
        var periodes = await _periodeRepository
            .GetAllAsync(p => p.AnneeScolaireId == anneeScolaireId && !p.IsDeleted);

        if (!periodes.Any())
            return;

        var fraisAGenerer = new List<Frais>();
        var dateActuelle = DateTime.UtcNow;

        foreach (var typeFrais in typesFraisRecurrents)
        {
            foreach (var periode in periodes)
            {
                // Ne générer les frais que pour les périodes futures ou en cours
                if (periode.DateFin >= dateActuelle.Date)
                {
                    var montant = typeFrais.MontantParDefaut ?? CalculerMontantParDefaut(typeFrais.Code);

                    var frais = new Frais
                    {
                        Id = Guid.NewGuid(),
                        EleveId = eleveId,
                        TypeFraisId = typeFrais.Id,
                        PeriodeId = periode.Id,
                        Montant = montant,
                        MontantPaye = 0,
                        DateEcheance = CalculerDateEcheance(periode),
                        Statut = StatutFrais.Impaye,
                        Observations = $"Frais généré automatiquement lors de l'inscription",
                        CreatedBy = "System"
                    };

                    fraisAGenerer.Add(frais);
                }
            }
        }

        // Sauvegarder tous les frais en une seule transaction
        if (fraisAGenerer.Any())
        {
            foreach (var frais in fraisAGenerer)
            {
                await _fraisRepository.AddAsync(frais);
            }
        }
    }

    /// <summary>
    /// Calcule le montant par défaut selon le type de frais
    /// </summary>
    private decimal CalculerMontantParDefaut(string codeFrais)
    {
        return codeFrais switch
        {
            "INSC" => 50000m,   // Inscription
            "SCOL" => 150000m,  // Scolarité
            "CANT" => 75000m,   // Cantine
            "TRANS" => 50000m,  // Transport
            _ => 0m
        };
    }

    /// <summary>
    /// Calcule la date d'échéance : 1 mois après le début de la période
    /// </summary>
    private DateTime CalculerDateEcheance(Periode periode)
    {
        return periode.DateDebut.AddMonths(1);
    }

    /// <summary>
    /// Recalcule le solde global de la famille après ajout d'un élève
    /// </summary>
    private async Task RecalculerSoldeFamilleAsync(Guid familleId, CancellationToken cancellationToken)
    {
        var famille = await _familleRepository.GetByIdAsync(familleId);
        if (famille == null)
            return;

        var eleves = await _eleveRepository.GetByFamilleAsync(familleId, cancellationToken);
        
        decimal totalDu = eleves
            .Where(e => !e.IsDeleted)
            .SelectMany(e => e.Frais)
            .Where(f => !f.IsDeleted)
            .Sum(f => f.Montant);

        decimal totalPaye = eleves
            .Where(e => !e.IsDeleted)
            .SelectMany(e => e.Frais)
            .Where(f => !f.IsDeleted)
            .Sum(f => f.MontantPaye);

        famille.SoldeGlobal = totalDu - totalPaye;
        famille.UpdatedAt = DateTime.UtcNow;
        famille.UpdatedBy = "System";

        await _familleRepository.UpdateAsync(famille);
    }
}