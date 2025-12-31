using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repositories;
using GameLibrary.Repository.Repository.Interface;
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
    }
}
