using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repositories;
using GameLibrary.Repository.Repositories.Interfaces;
using GameLibrary.Repository.Repository;
using GameLibrary.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameLibrary.Repository;

public static class DIConfig
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<GameLibraryDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IDeveloperRepository, DeveloperRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IPlatformRepository, PlatformRepository>();

        return services;
    }
}
