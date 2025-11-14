using MediatR;
using Microsoft.EntityFrameworkCore;
using GMS.Infrastructure.Repositories;
using GMS.Domain.Entities;
using System.Linq.Expressions;
using System;
using GMS.Application.Common;

namespace GMS.Application.Features.Familles.Queries.GetFamilles;

public class GetFamillesQueryHandler : IRequestHandler<GetFamillesQuery, Result<FamilleDto>>
{
    private readonly IFamilleRepository _familleRepository;
    private readonly IEleveRepository _eleveRepository;

    public GetFamillesQueryHandler(IFamilleRepository familleRepository, IEleveRepository eleveRepository)
    {
        _familleRepository = familleRepository;
        _eleveRepository = eleveRepository;
    }


    public async Task<Result<FamilleDto>> Handle(GetFamillesQuery request, CancellationToken cancellationToken)
    {
        // 1. Récupérer la famille avec navigation properties
        var famille = await _familleRepository.GetByIdWithDetailsAsync(request.FamilleId);
        if (famille == null)
        {
            return Result<FamilleDto>.Failure($"Famille avec l'ID {request.FamilleId} introuvable");
        }

        // 2. Récupérer tous les élèves actifs
        var eleves = famille.Eleves.Where(e => !e.IsDeleted).ToList();

        // 3. Calculer les statistiques financières
        var totalFrais = eleves.SelectMany(e => e.Frais).Sum(f => f.Montant);
        var totalPaye = eleves.SelectMany(e => e.Frais).Sum(f => f.MontantPaye);
        var fraisImpaye = eleves.SelectMany(e => e.Frais)
            .Where(f => f.Statut == "Impaye" || f.Statut == "Partiel")
            .ToList();
        var prochaineEcheance = fraisImpaye
            .Where(f => f.DateEcheance > DateTime.UtcNow)
            .OrderBy(f => f.DateEcheance)
            .FirstOrDefault()?.DateEcheance;

        // 4. Construire la réponse
        var response = new FamilleDto
        {
            Id = famille.Id,
            NomComplet = $"{famille.NomResponsable} {famille.PrenomResponsable}",
            TelephonePrincipal = famille.TelephonePrincipal,
            TelephoneSecondaire = famille.TelephoneSecondaire,
            Email = famille.Email,
            Adresse = famille.Adresse,
            Ville = famille.Ville,
            SoldeGlobal = famille.SoldeGlobal,
            NombreEnfants = eleves.Count,
            CreatedAt = famille.CreatedAt,
            Enfants = eleves.Select(e => new EleveDossierResponse
            {
                Eleve = new InfosPersonnelles
                {
                    Id = e.Id,
                    Matricule = e.Matricule,
                    NomComplet = $"{e.Nom} {e.Prenom}",
                    Statut = e.Statut,
                    PhotoPath = e.PhotoPath,
                    DateNaissance = e.DateNaissance,
                    Age = DateTime.Today.Year - e.DateNaissance.Year,
                    LieuNaissance = e.LieuNaissance,
                    Sexe = e.Sexe,
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
                    Classe = e.Classe?.Libelle ?? "Non assigné",
                    Niveau = e.Classe?.Niveau ?? "N/A",
                    AnneeScolaire = e.AnneeScolaire?.Libelle ?? "N/A"
                },

                Finances = new BulletinFinancier
                {
                    SoldeEleve = e.SoldeFinancier,
                    TotalFrais = e.Frais.Sum(f => f.Montant),
                    TotalPaye = e.Frais.Sum(f => f.MontantPaye),
                    SoldeRestant = e.Frais.Sum(f => f.Montant - f.MontantPaye),
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

                    }).ToList(),
            }).ToList(),
        };

        return Result<FamilleDto>.Success(response);
    }    

}