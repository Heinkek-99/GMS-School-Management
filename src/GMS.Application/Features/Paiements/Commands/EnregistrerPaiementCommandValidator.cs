using FluentValidation;
using GMS.Application.Features.Paiements.Commands;
using GMS.Shared.Constants;


public class EnregistrerPaiementCommandValidator : AbstractValidator<EnregistrerPaiementCommand>
{
    public EnregistrerPaiementCommandValidator()
    {
        RuleFor(x => x.FamilleId)
            .NotEmpty().WithMessage("La famille est requise");

        RuleFor(x => x.Montant)
            .GreaterThan(0).WithMessage("Le montant doit être supérieur à 0")
            .LessThanOrEqualTo(10000000).WithMessage("Le montant ne peut pas dépasser 10 000 000 FCFA");

        RuleFor(x => x.ModePaiement)
            .NotEmpty().WithMessage("Le mode de paiement est requis")
            .Must(m => ModePaiement.All.Contains(m))
            .WithMessage("Mode de paiement invalide");

        RuleFor(x => x.Ventilations)
            .NotEmpty().WithMessage("Au moins une ventilation est requise");

        RuleFor(x => x)
            .Must(cmd => cmd.Ventilations.Sum(v => v.MontantAffecte) == cmd.Montant)
            .WithMessage("La somme des ventilations doit être égale au montant total");

        RuleForEach(x => x.Ventilations).ChildRules(ventilation =>
        {
            ventilation.RuleFor(v => v.FraisId)
                .NotEmpty().WithMessage("L'identifiant du frais est requis");

            ventilation.RuleFor(v => v.MontantAffecte)
                .GreaterThan(0).WithMessage("Le montant de la ventilation doit être supérieur à 0");
        });
    }
}