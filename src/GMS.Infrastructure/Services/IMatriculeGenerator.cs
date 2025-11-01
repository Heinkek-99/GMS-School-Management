namespace GMS.Infrastructure.Services;
using System.Threading;
using System.Threading.Tasks;

public interface IMatriculeGenerator
{
    Task<string> GenerateAsync(Guid anneeScolaireId, CancellationToken cancellationToken = default);
}

