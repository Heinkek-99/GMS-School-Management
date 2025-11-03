
using FluentValidation;
using GMS.Application.Features.Familles.Commands;

namespace GMS.Application.Features.Familles.Commands.CreateFamille;

public class CreateFamilleCommandValidator : AbstractValidator<CreateFamilleCommand>
{
    public CreateFamilleCommandValidator()
    {
        RuleFor(x => x.NomFamille)
            .NotEmpty().WithMessage("Le nom de famille est requis")
            .MaximumLength(100);

        RuleFor(x => x.NomPere)
            .NotEmpty().WithMessage("Le nom du père est requis")
            .MaximumLength(100);

        RuleFor(x => x.NomMere)
            .NotEmpty().WithMessage("Le nom de la mère est requis")
            .MaximumLength(100);

        RuleFor(x => x.TelephonePere)
            .Matches(@"^\+?[\d\s\-()]+$").When(x => !string.IsNullOrEmpty(x.TelephonePere))
            .WithMessage("Format de téléphone invalide");

        RuleFor(x => x.EmailPere)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.EmailPere))
            .WithMessage("Format email invalide");

        RuleFor(x => x.Adresse)
            .NotEmpty().WithMessage("L'adresse est requise")
            .MaximumLength(255);
    }
}