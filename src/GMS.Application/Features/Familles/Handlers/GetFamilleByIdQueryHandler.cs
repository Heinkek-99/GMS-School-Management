using GMS.Application.Common;
using GMS.Application.Features.Familles.Queries;
using GMS.Infrastructure.Repositories;
using MediatR;

public class GetFamilleByIdQueryHandler : IRequestHandler<GetFamilleByIdQuery, Result<FamilleDto>>
{
    private readonly IFamilleRepository _familleRepository;

    public GetFamilleByIdQueryHandler(IFamilleRepository familleRepository)
    {
        _familleRepository = familleRepository;
    }

    public async Task<Result<FamilleDto>> Handle(GetFamilleByIdQuery request, CancellationToken cancellationToken)
    {
        var famille = await _familleRepository.GetWithElevesAsync(request.FamilleId, cancellationToken);
        
        if (famille == null)
            return Result<FamilleDto>.Failure("Famille introuvable");

        var dto = new FamilleDto
        {
            Id = famille.Id,
            CodeFamille = famille.CodeFamille,
            NomResponsable = famille.NomResponsable,
            PrenomResponsable = famille.PrenomResponsable ?? string.Empty,
            TelephonePrincipal = famille.TelephonePrincipal ?? "N/A",
            Email = famille.Email,
            Adresse = famille.Adresse,
            SoldeGlobal = famille.SoldeGlobal,
            StatutFinancier = famille.SoldeGlobal >= 0 ? "Créditeur" : "Débiteur",
            Enfants = famille.Eleves
                .Where(e => !e.IsDeleted)
                .Select(e => new EleveDossierResponse
                {
                    Eleve = new InfosPersonnelles
                    {
                        Id = e.Id,
                        Matricule = e.Matricule,
                        Nom = e.Nom,
                        Prenom = e.Prenom,
                        DateNaissance = e.DateNaissance,
                        Age = DateTime.Today.Year - e.DateNaissance.Year,
                        LieuNaissance = e.LieuNaissance,
                        Sexe = e.Sexe,
                        PhotoPath = e.PhotoPath,
                        Statut = e.Statut,
                        DateInscription = e.DateInscription
                    },
                    Famille = new InfosFamille
                    {
                        NomResponsable = famille.NomResponsable,
                        PrenomResponsable = famille.PrenomResponsable,
                        TelephonePrincipal = famille.TelephonePrincipal,
                        Email = famille.Email,
                        Adresse = famille.Adresse,
                        NombreEnfantsTotal = famille.Eleves.Count(ev => !ev.IsDeleted)
                    },
                    Scolarite = new InfosScolarite
                    {
                        Classe = e.Classe.Nom ?? "N/A",
                        Niveau = e.Classe.Niveau ?? "N/A",
                        AnneeScolaire = e.AnneeScolaire?.Libelle ?? "N/A",
                        EffectifClasse = e.Classe.Eleves.Count(ev => ev.Statut == "Actif" && !ev.IsDeleted)
                    },
                    Finances = new BulletinFinancier
                    {
                        TotalFrais = e.Frais.Sum(f => f.Montant),
                        TotalPaye = e.Frais.Sum(f => f.MontantPaye),
                        SoldeEleve = e.SoldeFinancier,
                        Frais = e.Frais.Select(f => new FraisDto
                        {
                            Id = f.Id,
                            TypeFrais = f.TypeFrais.Libelle,
                            Montant = f.Montant,
                            MontantPaye = f.MontantPaye,
                            Solde = f.Montant - f.MontantPaye,
                            DateEcheance = f.DateEcheance,
                            Statut = f.Statut
                        }).ToList(),

                        DerniersPaiements = e.Frais
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
                    DernieresNotes = e.Notes
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
                }).ToList()
        };          

        return Result<FamilleDto>.Success(dto);
    }
}