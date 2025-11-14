using FluentValidation;
using GMS.Application.Features.Familles.Commands;

public class CreateFamileCommandValidator : AbstractValidator<CreateFamilleCommand>
{
    public CreateFamileCommandValidator()
    {
        RuleFor(x => x.EcoleId).NotEmpty();
        
        RuleFor(x => x.NomResponsable)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.TelephonePrincipal)
            .NotEmpty()
            .MaximumLength(20);
        RuleFor(x => x.Email)
        .EmailAddress()
        .When(x => !string.IsNullOrEmpty(x.Email));
 
        RuleFor(x => x.PrenomPere)
            .NotEmpty().WithMessage("Le nom du contact est requis")
            .MaximumLength(100);

        RuleFor(x => x.NomPere)
            .NotEmpty().WithMessage("Le prénom du contact est requis")
            .MaximumLength(100);

        RuleFor(x => x.TelephonePere)
            .NotEmpty().WithMessage("Le numéro de téléphone est requis")
            .Matches(@"^\+?[\d\s\-()]+$").WithMessage("Format de téléphone invalide")
            .MaximumLength(20);

        RuleFor(x => x.EmailPere)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.EmailPere))
            .WithMessage("Format d'email invalide");

        RuleFor(x => x.PrenomMere)
            .NotEmpty().WithMessage("Le nom du contact est requis")
            .MaximumLength(100);

        RuleFor(x => x.NomMere)
            .NotEmpty().WithMessage("Le prénom du contact est requis")
            .MaximumLength(100);

        RuleFor(x => x.TelephoneMere)
            .NotEmpty().WithMessage("Le numéro de téléphone est requis")
            .Matches(@"^\+?[\d\s\-()]+$").WithMessage("Format de téléphone invalide")
            .MaximumLength(20);

        RuleFor(x => x.EmailMere)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.EmailMere))
            .WithMessage("Format d'email invalide");

        RuleFor(x => x.Adresse)
            .NotEmpty().WithMessage("L'adresse est requise")
            .MaximumLength(200);

        RuleFor(x => x.Ville)
            .MaximumLength(100);
        
    }
}