using GameLibrary.Database.Context;
using GameLibrary.Database.Entities;

namespace GameLibrary.Database.Repositories
{
    public class PlatformRepository : BaseRepository<Platform>
    {
        public PlatformRepository(GameLibraryDbContext dbContext) : base(dbContext)
        { }
    }
}
