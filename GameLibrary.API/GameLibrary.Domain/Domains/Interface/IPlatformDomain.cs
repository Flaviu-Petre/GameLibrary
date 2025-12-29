using GameLibrary.Entity.Entities;

namespace GameLibrary.Domain.Domains.Interface
{
    public interface IPlatformDomain
    {
        Task<IEnumerable<Platform>> GetAllPlatformsAsync();
        Task<Platform?> GetPlatformByIdAsync(int id);
        Task<Platform?> GetPlatformByNameAsync(string name);
        Task AddPlatformAsync(Platform platform);
        Task UpdatePlatformAsync(Platform platform);
        Task DeletePlatformAsync(int id);

    }
}
