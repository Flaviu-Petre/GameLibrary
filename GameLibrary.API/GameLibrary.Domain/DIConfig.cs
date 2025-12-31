using GameLibrary.Domain.Domains;
using GameLibrary.Domain.Domains.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace GameLibrary.Domain;

public static class DIConfig
{
    public static IServiceCollection AddDomains(this IServiceCollection services)
    {
        services.AddScoped<IDeveloperDomain, DeveloperDomain>();
        services.AddScoped<IGenreDomain, GenreDomain>();
        services.AddScoped<IPlatformDomain, PlatformDomain>();
        services.AddScoped<IPublisherDomain, PublisherDomain>();
        services.AddScoped<IUserDomain, UserDomain>();
        return services;
    }
}
