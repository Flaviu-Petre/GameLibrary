using GameLibrary.Database.Context;
using GameLibrary.Database.Entities;

namespace GameLibrary.Database.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(GameLibraryDbContext dbContext) : base(dbContext)
        { }
    }
}
