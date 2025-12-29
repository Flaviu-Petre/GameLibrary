using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repositories;
using GameLibrary.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Repository.Repository
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(GameLibraryDbContext context) : base(context)
        {
        }

        public async Task<Genre?> GetByNameAsync(string name)
        {
            return await GetQueryable().
                FirstOrDefaultAsync(g => g.Name == name);
        }
    }
}
