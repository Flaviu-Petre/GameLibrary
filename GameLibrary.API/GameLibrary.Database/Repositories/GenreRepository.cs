using GameLibrary.Database.Context;
using GameLibrary.Database.Entities;

namespace GameLibrary.Database.Repositories
{
    public class GenreRepository : BaseRepository<Genre>
    {
        public GenreRepository(GameLibraryDbContext dbContext) : base(dbContext)
        { }
    }
}
