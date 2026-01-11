using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repositories;
using GameLibrary.Repository.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Repository.Repository
{
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(GameLibraryDbContext context) : base(context)
        {
        }
        public async Task<Publisher?> GetByNameAsync(string name)
        {
            return await GetQueryable()
                .FirstOrDefaultAsync(p => p.Name == name);
        }
        public async Task<IEnumerable<Publisher>> GetByCountryAsync(string country)
        {
            var param = new SqlParameter("@Country", country);

            return await _dbSet
                .FromSqlRaw("EXEC sp_GetPublishersByCountry @Country", param)
                .IgnoreQueryFilters()
                .ToListAsync();
        }

        public async Task<IEnumerable<Publisher>> GetPaginatedAsync(int page, int pageSize)
        {
            var pPage = new SqlParameter("@Page", page);
            var pPageSize = new SqlParameter("@PageSize", pageSize);

            return await _dbSet
                .FromSqlRaw("EXEC sp_GetPublishersPaginated @Page, @PageSize", pPage, pPageSize)
                .IgnoreQueryFilters()
                .ToListAsync();
        }
    }
}
