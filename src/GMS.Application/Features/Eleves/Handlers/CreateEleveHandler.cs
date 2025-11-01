using GMS.Application.Common;
using GMS.Application.Features.Eleves.Commands;
using GMS.Domain.Entities;
using GMS.Infrastructure.Data;
using GMS.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GMS.Application.Features.Eleves.Handlers;

public class CreateEleveHandler : IRequestHandler<CreateEleveCommand, Result<Guid>>
{
    private readonly GmsDbContext _context;
    private readonly IGenericRepository<Eleve> _eleveRepo;

    public CreateEleveHandler(GmsDbContext context, IGenericRepository<Eleve> eleveRepo)
    {
        _context = context;
        _eleveRepo = eleveRepo;
    }

    public async Task<Result<Guid>> Handle(CreateEleveCommand request, CancellationToken cancellationToken)
    {
        // Générer le matricule unique
        var anneeScolaire = await _context.AnneesScolaires
            .FirstOrDefaultAsync(a => a.Id == request.AnneeScolaireId, cancellationToken);

        if (anneeScolaire == null)
            return Result<Guid>.Failure("Année scolaire invalide");

        var annee = anneeScolaire.Libelle.Split('-')[0];
        var compteur = await _context.Eleves.CountAsync(cancellationToken) + 1;
        var matricule = $"EL{annee}{compteur:D5}"; // Ex: EL202400123

        var eleve = new Eleve
        {
            Id = Guid.NewGuid(),
            Matricule = matricule,
            FamilleId = request.FamilleId,
            Nom = request.Nom.ToUpper(),
            Prenom = request.Prenom,
            DateNaissance = request.DateNaissance,
            LieuNaissance = request.LieuNaissance,
            Sexe = request.Sexe,
            ClasseId = request.ClasseId,
            AnneeScolaireId = request.AnneeScolaireId,
            PhotoPath = request.PhotoPath,
            DateInscription = DateTime.Now,
            Statut = "Actif"
        };

        await _eleveRepo.AddAsync(eleve);

        // Générer automatiquement les frais standards
        await GenererFraisStandards(eleve.Id, request.ClasseId);

        return Result<Guid>.Success(eleve.Id);
    }

    private async Task GenererFraisStandards(Guid eleveId, Guid classeId)
    {
        // Récupérer les types de frais standards
        var typesFrais = await _context.TypesFrais
            .Where(t => !t.IsDeleted)
            .ToListAsync();

        var fraisAGenerer = new List<Frais>();
        var dateActuelle = DateTime.Now;

        foreach (var typeFrais in typesFrais)
        {
            // Montants par défaut (à personnaliser selon la classe)
            decimal montant = typeFrais.Code switch
            {
                "INSC" => 50000, // Inscription
                "SCOL" => 150000, // Scolarité annuelle
                "CANT" => 30000, // Cantine (si applicable)
                _ => 0
            };

            if (montant > 0)
            {
                var frais = new Frais
                {
                    Id = Guid.NewGuid(),
                    EleveId = eleveId,
                    TypeFraisId = typeFrais.Id,
                    Montant = montant,
                    DateEcheance = dateActuelle.AddMonths(1),
                    Statut = "Impayé",
                    MontantPaye = 0
                };

                fraisAGenerer.Add(frais);
            }
        }

        await _context.Frais.AddRangeAsync(fraisAGenerer);
        await _context.SaveChangesAsync();
    }
}
