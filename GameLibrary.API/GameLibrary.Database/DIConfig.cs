using GameLibrary.Database.Context;
using GameLibrary.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameLibrary.Database
{
    static public class DIConfig
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddDbContext<GameLibraryDbContext>();
            services.AddScoped<DbContext, GameLibraryDbContext>();

            services.AddScoped<UserRepository>();
            services.AddScoped<PlatformRepository>();
            services.AddScoped<PublisherRepository>();
            services.AddScoped<DeveloperRepository>();
            services.AddScoped<GameRepository>();
            services.AddScoped<GenreRepository>();

            return services;
        }
    }
}
