using MediatR;
using Microsoft.EntityFrameworkCore;
using GMS.Infrastructure.Data;

namespace GMS.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly GmsDbContext _context;

    public LoginCommandHandler(GmsDbContext context)
    {
        _context = context;
    }

    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Rechercher l'utilisateur
            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

            if (utilisateur == null)
            {
                return new LoginResult
                {
                    Success = false,
                    ErrorMessage = "Nom d'utilisateur ou mot de passe incorrect"
                };
            }

            // Vérifier si le compte est actif
            if (!utilisateur.EstActif)
            {
                return new LoginResult
                {
                    Success = false,
                    ErrorMessage = "Ce compte a été désactivé. Contactez l'administrateur."
                };
            }

            // Vérifier le mot de passe avec BCrypt
            bool isPasswordValid = false;
            try
            {
                isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, utilisateur.PasswordHash);
            }
            catch
            {
                // Si erreur BCrypt, mot de passe invalide
                isPasswordValid = false;
            }

            if (!isPasswordValid)
            {
                return new LoginResult
                {
                    Success = false,
                    ErrorMessage = "Nom d'utilisateur ou mot de passe incorrect"
                };
            }

            // Mettre à jour la dernière connexion
            utilisateur.DerniereConnexion = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);

            // Connexion réussie
            return new LoginResult
            {
                Success = true,
                Utilisateur = new UtilisateurDto
                {
                    Id = utilisateur.Id,
                    Nom = utilisateur.Nom,
                    Prenom = utilisateur.Prenom,
                    Email = utilisateur.Email,
                    Username = utilisateur.Username,
                    Role = utilisateur.Role,
                    NomComplet = utilisateur.NomComplet
                }
            };
        }
        catch (Exception ex)
        {
            return new LoginResult
            {
                Success = false,
                ErrorMessage = $"Erreur de connexion: {ex.Message}"
            };
        }
    }
}