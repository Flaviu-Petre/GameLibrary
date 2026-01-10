using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repositories;
using GameLibrary.Repository.Repository.Interface;
using Microsoft.Data.SqlClient;
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

        public async Task<IEnumerable<Platform>> SP_GetPlatformsPaginatedAsync(int pageNumber, int pageSize)
        {
            var pageNumberParam = new SqlParameter("@PageNumber", pageNumber);
            var pageSizeParam = new SqlParameter("@PageSize", pageSize);

            return await _dbSet
                .FromSqlRaw("EXEC sp_GetPlatformsPaginated @PageNumber, @PageSize", pageNumberParam, pageSizeParam)
                .IgnoreQueryFilters()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Platform>> SP_SearchPlatformsByNameAsync(string nameTerm)
        {
            var nameParam = new SqlParameter("@NameTerm", nameTerm ?? string.Empty);

            return await _dbSet
                .FromSqlRaw("EXEC sp_SearchPlatformsByName @NameTerm", nameParam)
                .IgnoreQueryFilters()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Platform>> SP_GetPlatformsByReleaseYearAsync(int releaseYear)
        {
            var yearParam = new SqlParameter("@ReleaseYear", releaseYear);

            return await _dbSet
                .FromSqlRaw("EXEC sp_GetPlatformsByReleaseYear @ReleaseYear", yearParam)
                .IgnoreQueryFilters()
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
