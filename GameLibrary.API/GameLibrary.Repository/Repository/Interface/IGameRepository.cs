using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Repositories.Interfaces;

namespace GameLibrary.Repository.Repository.Interface
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<Game?> GetByIdAsync(int id, bool includeDeleted = false);
        Task<IEnumerable<Game>> GetAllAsync(bool includeDeleted = false);
        Task<Game?> GetByTitleAsync(string title);
    }
}
