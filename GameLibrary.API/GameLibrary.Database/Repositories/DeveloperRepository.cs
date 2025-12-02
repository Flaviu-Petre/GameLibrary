using GameLibrary.Database.Context;
using GameLibrary.Database.Entities;

namespace GameLibrary.Database.Repositories
{
    public class DeveloperRepository : BaseRepository<Developer>
    {
        public DeveloperRepository(GameLibraryDbContext dbContext) : base(dbContext)
        { }
    }
}
