namespace GMS.Infrastructure.Services;

public interface IAuthService
{
    Task<AuthResult> LoginAsync(string username, string password);
    Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}

public record AuthResult
{
    public bool IsSuccess { get; init; }
    public Guid UserId { get; init; }
    public string Username { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public string? ErrorMessage { get; init; }
}
