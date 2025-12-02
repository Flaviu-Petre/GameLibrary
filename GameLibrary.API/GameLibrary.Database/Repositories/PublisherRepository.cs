using GameLibrary.Database.Context;
using GameLibrary.Database.Entities;

namespace GameLibrary.Database.Repositories
{
    public class PublisherRepository : BaseRepository<Publisher>
    {
        public PublisherRepository(GameLibraryDbContext dbContext) : base(dbContext)
        { }
    }
}
