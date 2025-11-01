namespace GMS.Application.Services;

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
    public string Username { get; init; }
    public string Role { get; init; }
    public string FullName { get; init; }
    public string? ErrorMessage { get; init; }
}