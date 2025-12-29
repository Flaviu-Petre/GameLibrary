using GameLibrary.Service.Services;
using GameLibrary.Service.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace GameLibrary.Service;

public static class DIConfig
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDeveloperService, DeveloperService>();
        return services;
    }
}
