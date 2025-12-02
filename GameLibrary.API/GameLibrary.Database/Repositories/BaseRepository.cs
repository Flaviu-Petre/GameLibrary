using GameLibrary.Database.Context;
using GameLibrary.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Database.Repositories
{
    public class BaseRepository<T> where T : BaseEntity
    {
        protected readonly GameLibraryDbContext _context;
        private DbSet<T> DbSet { get; }
        public BaseRepository(GameLibraryDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public Task<List<T>> GetAllAsync(bool includeDeletedEntities = false)
        {
            return GetRecords(includeDeletedEntities).ToListAsync();
        }

        public Task<T?> GetFirstOrDefaultAsync(int primaryKey, bool includeDeletedEntities = false)
        {
            var records = GetRecords(includeDeletedEntities);
            return records.FirstOrDefaultAsync(record => record.Id == primaryKey);
        }

        protected IQueryable<T> GetRecords(bool includeDeletedEntities = false)
        {
            var result = DbSet.AsQueryable();
            if (includeDeletedEntities is false)
            {
                result = result.Where(r => r.DeletedAt == null);
            }
            return result;
        }

        public void Insert(params T[] records)
        {
            DbSet.AddRange(records);
        }

        public void Update(params T[] records)
        {
            foreach (var baseEntity in records)
            {
                baseEntity.ModifiedAt = DateTime.UtcNow;
            }
            DbSet.UpdateRange(records);
        }

        public void SoftDelete(params T[] records)
        {
            foreach (var baseEntity in records)
            {
                baseEntity.DeletedAt = DateTime.UtcNow;
            }
            Update(records);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
