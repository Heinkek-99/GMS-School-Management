namespace GMS.Infrastructure.Services;

public interface INumeroPaiementGenerator
{
    Task<string> GenerateAsync(CancellationToken cancellationToken = default);
}