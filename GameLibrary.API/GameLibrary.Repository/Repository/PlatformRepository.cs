using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repositories;
using GameLibrary.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Repository.Repository
{
    public class PlatformRepository : BaseRepository<Platform>, IPlatformRepository
    {
        public PlatformRepository(GameLibraryDbContext context) : base(context)
        {
        }
        public async Task<Platform?> GetByNameAsync(string name)
        {
            return await GetQueryable()
                .FirstOrDefaultAsync(p => p.Name == name);
        }
        public override async Task<Platform?> GetByIdAsync(int id, bool includeDeleted = false)
        {
            return await GetQueryable(includeDeleted)
                .Include(p => p.Games)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
