using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repositories;
using GameLibrary.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Repository.Repository
{
    public class DeveloperRepository : BaseRepository<Developer>, IDeveloperRepository
    {
        public DeveloperRepository(GameLibraryDbContext context) : base(context)
        {
        }

        public async Task<Developer?> GetByNameAsync(string name)
        {
            return await GetQueryable()
                .FirstOrDefaultAsync(d => d.Name == name);
        }

        public override async Task<Developer?> GetByIdAsync(int id, bool includeDeleted = false)
        {
            return await GetQueryable(includeDeleted)
                .Include(d => d.Games)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
