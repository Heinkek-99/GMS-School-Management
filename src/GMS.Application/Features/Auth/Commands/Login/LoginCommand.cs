using MediatR;
using FluentValidation;

namespace GMS.Application.Features.Auth.Commands.Login;

public record LoginCommand : IRequest<LoginResult>
{
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

public record LoginResult
{
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
    public UtilisateurDto? Utilisateur { get; init; }
}

public record UtilisateurDto
{
    public Guid Id { get; init; }
    public string Nom { get; init; } = string.Empty;
    public string Prenom { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public string NomComplet { get; init; } = string.Empty;
}

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Le nom d'utilisateur est requis")
            .MinimumLength(3).WithMessage("Minimum 3 caractères");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Le mot de passe est requis")
            .MinimumLength(6).WithMessage("Minimum 6 caractères");
    }
}