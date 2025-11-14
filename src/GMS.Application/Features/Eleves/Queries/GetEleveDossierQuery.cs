using GMS.Application.Common;
using GMS.Infrastructure.Repositories;
using MediatR;

namespace GMS.Application.Features.Eleves.Queries;

public class GetEleveDossierQuery : IRequest<Result<EleveDossierResponse>>
{
    public Guid EleveId { get; set; }

    public GetEleveDossierQuery(Guid eleveId)
    {
        EleveId = eleveId;
    }

    public class GetEleveDossierQueryHandler : IRequestHandler<GetEleveDossierQuery, Result<EleveDossierResponse>>
    {
        private readonly IEleveRepository _eleveRepository;

        public GetEleveDossierQueryHandler(IEleveRepository eleveRepository)
        {
            _eleveRepository = eleveRepository;
        }

        public async Task<Result<EleveDossierResponse>> Handle(GetEleveDossierQuery request, CancellationToken cancellationToken)
        {
            var eleve = await _eleveRepository.GetDossierCompletAsync(request.EleveId, cancellationToken);

            if (eleve == null)
                return Result<EleveDossierResponse>.Failure("Élève introuvable");

            var dossier = new EleveDossierResponse
            {
                Eleve = new InfosPersonnelles
                {
                    Id = eleve.Id,
                    Matricule = eleve.Matricule,
                    Nom = eleve.Nom,
                    Prenom = eleve.Prenom,
                    DateNaissance = eleve.DateNaissance,
                    Age = DateTime.Today.Year - eleve.DateNaissance.Year,
                    LieuNaissance = eleve.LieuNaissance,
                    Sexe = eleve.Sexe,
                    PhotoPath = eleve.PhotoPath,
                    Statut = eleve.Statut,
                    DateInscription = eleve.DateInscription
                },
                Famille = new InfosFamille
                {
                    NomResponsable = eleve.Famille.NomResponsable,
                    PrenomResponsable = eleve.Famille.PrenomResponsable,
                    TelephonePrincipal = eleve.Famille.TelephonePrincipal,
                    Email = eleve.Famille.Email,
                    Adresse = eleve.Famille.Adresse,
                    NombreEnfantsTotal = eleve.Famille.Eleves.Count(e => !e.IsDeleted)
                },
                Scolarite = new InfosScolarite
                {
                    Classe = eleve.Classe.Nom,
                    Niveau = eleve.Classe.Niveau,
                    AnneeScolaire = eleve.AnneeScolaire.Libelle,
                    EffectifClasse = eleve.Classe.Eleves.Count(e => e.Statut == "Actif" && !e.IsDeleted)
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
                        Montant = f.Montant,
                        MontantPaye = f.MontantPaye,
                        Solde = f.Montant - f.MontantPaye,
                        DateEcheance = f.DateEcheance,
                        Statut = f.Statut
                    }).ToList(),

                    DerniersPaiements = eleve.Frais
                        .SelectMany(f => f.Ventilations)
                        .Select(v => new PaiementDto
                        {
                            NumeroPaiement = v.Paiement.NumeroPaiement,
                            MontantVentile = v.Paiement.Montant,
                            DatePaiement = v.Paiement.DatePaiement,
                            ModePaiement = v.Paiement.ModePaiement
                        })
                        .OrderByDescending(p => p.DatePaiement)
                        .Take(5)
                        .ToList()
                },
                DernieresNotes = eleve.Notes
                    .OrderByDescending(n => n.DateEvaluation)
                    .Take(5)
                    .Select(n => new NoteDto
                    {
                        Matiere = n.Matiere.Libelle,
                        Periode = n.Periode.Libelle,
                        Date = n.DateEvaluation,
                        Note = n.Valeur,
                        NoteSur = n.NoteSur
                    }).ToList()
            };

            return Result<EleveDossierResponse>.Success(dossier);
        }
    }
}