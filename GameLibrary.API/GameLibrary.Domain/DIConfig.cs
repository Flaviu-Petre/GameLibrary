using GameLibrary.Domain.Domains;
using Microsoft.Extensions.DependencyInjection;

namespace GameLibrary.Domain;

public static class DIConfig
{
    public static IServiceCollection AddDomains(this IServiceCollection services)
    {
        services.AddScoped<DeveloperDomain>();

        return services;
    }
}
