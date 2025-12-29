using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Repositories.Interfaces;

namespace GameLibrary.Repository.Repository.Interface
{
    public interface IPlatformRepository : IRepository<Platform>
    {
        Task<Platform?> GetByNameAsync(string name);

    }
}
