using GMS.Application.Common;
using GMS.Application.Features.Eleves.Queries;
using GMS.Infrastructure.Repositories;
using MediatR;

namespace GMS.Application.Features.Eleves.Handlers;

/// <summary>
/// Handler pour récupérer le dossier complet d'un élève
/// </summary>
public class GetEleveDossierQueryHandler : IRequestHandler<GetEleveDossierQuery, Result<EleveDossierResponse>>
{
    private readonly IEleveRepository _eleveRepository;

    public GetEleveDossierQueryHandler(IEleveRepository eleveRepository)
    {
        _eleveRepository = eleveRepository;
    }

    public async Task<Result<EleveDossierResponse>> Handle(GetEleveDossierQuery request, CancellationToken cancellationToken)
    {
        // 1. Récupérer l'élève avec toutes les données liées
        var eleve = await _eleveRepository.GetDossierCompletAsync(request.EleveId, cancellationToken);
        
        if (eleve == null)
        {
            return Result<EleveDossierResponse>.Failure($"Élève avec l'ID {request.EleveId} introuvable");
        }

        // 2. Calculer l'âge
        var age = DateTime.Today.Year - eleve.DateNaissance.Year;
        if (eleve.DateNaissance.Date > DateTime.Today.AddYears(-age)) age--;

        // 3. Construire le dossier complet
        var response = new EleveDossierResponse
        {
            Eleve = new InfosPersonnelles
            {
                Id = eleve.Id,
                Matricule = eleve.Matricule,
                Nom = eleve.Nom,
                Prenom = eleve.Prenom,
                DateNaissance = eleve.DateNaissance,
                Age = age,
                LieuNaissance = eleve.LieuNaissance,
                Sexe = eleve.Sexe,
                PhotoPath = eleve.PhotoPath,
                Statut = eleve.Statut,
                DateInscription = eleve.DateInscription
            },
            Famille = new InfosFamille
            {
                FamilleId = eleve.FamilleId,
                NomResponsable = eleve.Famille.NomResponsable,
                PrenomResponsable = eleve.Famille.PrenomResponsable,
                TelephonePrincipal = eleve.Famille.TelephonePrincipal,
                Email = eleve.Famille.Email,
                Adresse = eleve.Famille.Adresse,
                NombreEnfantsTotal = eleve.Famille.Eleves.Count(e => !e.IsDeleted)
            },
            Scolarite = new InfosScolarite
            {
                Classe = eleve.Classe.Libelle,
                Niveau = eleve.Classe.Niveau,
                AnneeScolaire = eleve.Classe.AnneeScolaire.Libelle,
                EffectifClasse = eleve.Classe.Eleves.Count(e => !e.IsDeleted && e.Statut == "Actif")
            },
            Finances = new BulletinFinancier
            {
                SoldeEleve = eleve.SoldeFinancier,
                TotalFrais = eleve.Frais.Sum(f => f.Montant),
                TotalPaye = eleve.Frais.Sum(f => f.MontantPaye),
                SoldeRestant = eleve.Frais.Sum(f => f.Montant - f.MontantPaye),
                Frais = eleve.Frais.Select(f => new FraisDto
                {
                    Id = f.Id,
                    TypeFrais = f.TypeFrais.Libelle,
                    Periode = f.Periode.Libelle,
                    Montant = f.Montant,
                    MontantPaye = f.MontantPaye,
                    Solde = f.Solde,
                    Statut = f.Statut,
                    DateEcheance = f.DateEcheance,
                    EstEnRetard = f.DateEcheance < DateTime.Today && f.Statut != "Paye"
                }).OrderBy(f => f.DateEcheance).ToList(),

                DerniersPaiements = eleve.Frais
                    .SelectMany(f => f.Ventilations)
                    .OrderByDescending(v => v.Paiement.DatePaiement)
                    .Take(10)
                    .Select(v => new PaiementDto
                    {
                        NumeroPaiement = v.Paiement.NumeroPaiement,
                        DatePaiement = v.Paiement.DatePaiement,
                        MontantVentile = v.MontantAffecte,
                        ModePaiement = v.Paiement.ModePaiement
                    })
                    .ToList()
            },
            DernieresNotes = eleve.Notes
                .OrderByDescending(n => n.CreatedAt)
                .Take(10)
                .Select(n => new NoteDto
                {
                    Matiere = n.Matiere.Libelle ?? "Inconnu",
                    Note = n.Valeur,
                    Periode = n.Periode.Libelle ?? "Inconnu",
                    Date = n.CreatedAt
                })
                .ToList()
        };

        return Result<EleveDossierResponse>.Success(response);
    }
}