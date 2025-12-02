using GameLibrary.Database.Context;
using GameLibrary.Database.Entities;

namespace GameLibrary.Database.Repositories
{
    public class GameRepository : BaseRepository<Game>
    {
        public GameRepository(GameLibraryDbContext dbContext) : base(dbContext)
        { }
    }
}
