using FluentValidation;
using GMS.Application.Features.Eleves.Commands;

namespace GMS.Application.Validators;

public class CreateEleveValidator : AbstractValidator<CreateEleveCommand>
{
    public CreateEleveValidator()
    {
        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom est obligatoire")
            .MaximumLength(100).WithMessage("Le nom ne peut dépasser 100 caractères");

        RuleFor(x => x.Prenom)
            .NotEmpty().WithMessage("Le prénom est obligatoire")
            .MaximumLength(100).WithMessage("Le prénom ne peut dépasser 100 caractères");

        RuleFor(x => x.DateNaissance)
            .NotEmpty().WithMessage("La date de naissance est obligatoire")
            .LessThan(DateTime.Now).WithMessage("La date de naissance doit être dans le passé")
            .GreaterThan(DateTime.Now.AddYears(-25)).WithMessage("L'élève doit avoir moins de 25 ans");

        RuleFor(x => x.Sexe)
            .NotEmpty().WithMessage("Le sexe est obligatoire")
            .Must(x => x == "M" || x == "F").WithMessage("Le sexe doit être M ou F");

        RuleFor(x => x.FamilleId)
            .NotEmpty().WithMessage("La famille est obligatoire");

        RuleFor(x => x.ClasseId)
            .NotEmpty().WithMessage("La classe est obligatoire");

        RuleFor(x => x.AnneeScolaireId)
            .NotEmpty().WithMessage("L'année scolaire est obligatoire");
    }
}