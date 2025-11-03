using GMS.Infrastructure.Data;
using GMS.Infrastructure.Repositories;
using GMS.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GMS.Infrastructure;

/// <summary>
/// Configuration de l'injection de dépendances pour la couche Infrastructure
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        // DbContext
        services.AddDbContext<GmsDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(GmsDbContext).Assembly.FullName)
            ));
        
        // Repositories (si vous utilisez le Repository Pattern)
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


        return services;

    }
}