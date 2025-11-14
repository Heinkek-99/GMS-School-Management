using MediatR;
using FluentValidation;
using GMS.Application.Common;
using GMS.Application.Features.Eleves.Commands;

public class CreateEleveCommandValidator : AbstractValidator<CreateEleveCommand>
{
    public CreateEleveCommandValidator()
    {
        RuleFor(x => x.FamilleId)
            .NotEmpty().WithMessage("La famille est requise");

        RuleFor(x => x.ClasseId)
            .NotEmpty().WithMessage("La classe est requise");

        RuleFor(x => x.Nom)
            .NotEmpty().WithMessage("Le nom est requis")
            .MaximumLength(100).WithMessage("Le nom ne peut pas dépasser 100 caractères");

        RuleFor(x => x.Prenom)
            .NotEmpty().WithMessage("Le prénom est requis")
            .MaximumLength(100).WithMessage("Le prénom ne peut pas dépasser 100 caractères");

        RuleFor(x => x.DateNaissance)
            .NotEmpty().WithMessage("La date de naissance est requise")
            .LessThan(DateTime.Today).WithMessage("La date de naissance doit être dans le passé")
            .GreaterThan(DateTime.Today.AddYears(-25)).WithMessage("L'âge ne peut pas dépasser 25 ans");

        RuleFor(x => x.LieuNaissance)
            .NotEmpty().WithMessage("Le lieu de naissance est requis")
            .MaximumLength(100).WithMessage("Le lieu de naissance ne peut pas dépasser 100 caractères");

        RuleFor(x => x.Sexe)
            .NotEmpty().WithMessage("Le sexe est requis")
            .Must(s => s == "M" || s == "F").WithMessage("Le sexe doit être 'M' ou 'F'");
    }
}