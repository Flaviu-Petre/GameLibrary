using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Repositories.Interfaces;

namespace GameLibrary.Repository.Repository.Interface
{
    public interface IPlatformRepository : IRepository<Platform>
    {
        Task<Platform?> GetByNameAsync(string name);
        Task<IEnumerable<Platform>> SP_GetPlatformsPaginatedAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Platform>> SP_SearchPlatformsByNameAsync(string nameTerm);
        Task<IEnumerable<Platform>> SP_GetPlatformsByReleaseYearAsync(int releaseYear);
    }
}
