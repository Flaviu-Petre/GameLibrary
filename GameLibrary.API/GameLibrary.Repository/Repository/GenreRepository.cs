using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repositories;
using GameLibrary.Repository.Repository.Interface;
using Microsoft.Data.SqlClient;
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

        public override async Task<IEnumerable<Genre>> GetAllAsync(bool includeDeleted = false)
        {
            return await GetQueryable(includeDeleted)
                .Include(g => g.Games)
                .ToListAsync();
        }

        public override async Task<Genre?> GetByIdAsync(int id, bool includeDeleted = false)
        {
            return await GetQueryable(includeDeleted)
                .Include(g => g.Games)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Genre>> SP_GetGenresByPartialNameAsync(string nameTerm)
        {
            var nameParam = new SqlParameter("@NameTerm", nameTerm ?? string.Empty);

            return await _dbSet
                .FromSqlRaw("EXEC sp_GetGenresByPartialName @NameTerm", nameParam)
                .IgnoreQueryFilters()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Genre>> SP_GetGenresPaginatedAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var pageNumberParam = new SqlParameter("@PageNumber", pageNumber);
            var pageSizeParam = new SqlParameter("@PageSize", pageSize);

            return await _dbSet
                .FromSqlRaw("EXEC sp_GetGenresPaginated @PageNumber, @PageSize", pageNumberParam, pageSizeParam)
                .IgnoreQueryFilters()
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
