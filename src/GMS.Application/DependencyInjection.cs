using FluentValidation;
using GMS.Application.Common.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // FluentValidation
        services.AddValidatorsFromAssembly(assembly);

        // Services
        services.AddSingleton<ICurrentUserService, CurrentUserService>();


        return services;
    }
}