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
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(GmsDbContext).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });

            // Enable sensitive data logging in development
#if DEBUG
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
#endif
        });

        // Repositories
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IFamilleRepository, FamilleRepository>();
        services.AddScoped<IEleveRepository, EleveRepository>();
        services.AddScoped<IPaiementRepository, PaiementRepository>();

        // Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IMatriculeGenerator, MatriculeGenerator>();
        services.AddScoped<INumeroPaiementGenerator, NumeroPaiementGenerator>();

        return services;
    }
}