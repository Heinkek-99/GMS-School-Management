using GMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GMS.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly GmsDbContext _context;

    public AuthService(GmsDbContext context)
    {
        _context = context;
    }

    public async Task<AuthResult> LoginAsync(string username, string password)
    {
        var user = await _context.Utilisateurs
            .FirstOrDefaultAsync(u => u.Username == username && u.EstActif);

        if (user == null)
            return new AuthResult { IsSuccess = false, ErrorMessage = "Nom d'utilisateur ou mot de passe incorrect" };

        if (!VerifyPassword(password, user.PasswordHash))
            return new AuthResult { IsSuccess = false, ErrorMessage = "Nom d'utilisateur ou mot de passe incorrect" };

        // Mettre à jour dernière connexion
        user.DerniereConnexion = DateTime.Now;
        await _context.SaveChangesAsync();

        return new AuthResult
        {
            IsSuccess = true,
            UserId = user.Id,
            Username = user.Username,
            Role = user.Role,
            FullName = $"{user.Prenom} {user.Nom}"
        };
    }

    public async Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
    {
        var user = await _context.Utilisateurs.FindAsync(userId);
        if (user == null || !VerifyPassword(oldPassword, user.PasswordHash))
            return false;

        user.PasswordHash = HashPassword(newPassword);
        await _context.SaveChangesAsync();
        return true;
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
