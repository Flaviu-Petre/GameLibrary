using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Repositories.Interfaces;

namespace GameLibrary.Repository.Repository.Interface
{
    public class PaginatedResult<T>
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; } = new();
    }

    public interface IDeveloperRepository : IRepository<Developer>
    {
        Task<Developer?> GetByNameAsync(string name);

        Task<IEnumerable<Developer>> SP_GetDevelopersByCountryAsync(string country);

        Task<IEnumerable<Developer>> SP_GetDevelopersPaginatedAsync(int pageNumber, int pageSize);
    }
}
