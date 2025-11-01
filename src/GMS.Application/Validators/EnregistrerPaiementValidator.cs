using FluentValidation;
using GMS.Application.Features.Paiements.Commands;

namespace GMS.Application.Validators;

public class EnregistrerPaiementValidator : AbstractValidator<EnregistrerPaiementCommand>
{
    public EnregistrerPaiementValidator()
    {
        RuleFor(x => x.FamilleId)
            .NotEmpty().WithMessage("La famille est obligatoire");

        RuleFor(x => x.Montant)
            .GreaterThan(0).WithMessage("Le montant doit être supérieur à 0");

        RuleFor(x => x.DatePaiement)
            .NotEmpty().WithMessage("La date de paiement est obligatoire")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("La date ne peut être dans le futur");

        RuleFor(x => x.ModePaiement)
            .NotEmpty().WithMessage("Le mode de paiement est obligatoire");

        RuleFor(x => x.Ventilations)
            .NotEmpty().WithMessage("Au moins une ventilation est requise")
            .Must((cmd, ventilations) => ventilations.Sum(v => v.MontantAffecte) == cmd.Montant)
            .WithMessage("Le total des ventilations doit égaler le montant payé");
    }
}