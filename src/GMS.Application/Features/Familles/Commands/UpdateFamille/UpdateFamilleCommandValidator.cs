using FluentValidation;

public class UpdateFamilleCommandValidator : AbstractValidator<UpdateFamilleCommand>
{
    public UpdateFamilleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("L'identifiant de la famille est requis");

        RuleFor(x => x.NomResponsable)
            .NotEmpty().WithMessage("Le nom du responsable est requis")
            .MaximumLength(100).WithMessage("Le nom ne peut pas dépasser 100 caractères");

        RuleFor(x => x.PrenomResponsable)
            .NotEmpty().WithMessage("Le prénom du responsable est requis")
            .MaximumLength(100).WithMessage("Le prénom ne peut pas dépasser 100 caractères");

        RuleFor(x => x.TelephonePrincipal)
            .NotEmpty().WithMessage("Le téléphone principal est requis")
            .Matches(@"^\+?[0-9\s\-]{8,20}$").WithMessage("Format de téléphone invalide");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Format d'email invalide")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));
    }
}