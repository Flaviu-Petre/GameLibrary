using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repositories;
using GameLibrary.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Repository.Repository
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(GameLibraryDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Game>> GetAllAsync(bool includeDeleted = false)
        {
            return await GetQueryable(includeDeleted)
                .Include(g => g.Developer)
                .Include(g => g.Publisher)
                .Include(g => g.Platform)
                .Include(g => g.Genres)
                .ToListAsync();
        }
    }
}
