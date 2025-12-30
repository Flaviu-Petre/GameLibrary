using GameLibrary.Entity.Entities;

namespace GameLibrary.Domain.Domains.Interface
{
    public interface IDeveloperDomain
    {
        Task<IEnumerable<Developer>> GetAllDevelopersAsync();
        Task<Developer?> GetDeveloperByIdAsync(int id);
        Task<Developer?> GetDeveloperByNameAsync(string name);
        Task AddDeveloperAsync(Developer developer);
        Task UpdateDeveloperAsync(Developer developer);
        Task DeleteDeveloperAsync(int id);

        Task<IEnumerable<Developer>> SP_GetDevelopersByCountryAsync(string country);
    }
}
