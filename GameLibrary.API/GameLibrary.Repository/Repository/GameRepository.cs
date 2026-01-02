using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repositories;
using GameLibrary.Repository.Repository.Interface;

namespace GameLibrary.Repository.Repository
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(GameLibraryDbContext context) : base(context)
        {
        }
    }
}
