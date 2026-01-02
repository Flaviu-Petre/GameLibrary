using GameLibrary.Entity.Entities;
using GameLibrary.Integration.Exceptions;
using GameLibrary.Integration.Logger;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Repository.Repositories;

public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly GameLibraryDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(GameLibraryDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(int id, bool includeDeleted = false)
    {
        var query = GetQueryable(includeDeleted);
        var entity = await query.FirstOrDefaultAsync(e => e.Id == id);
        
        if (entity == null)
        {
            LoggerSingleton.GetInstance().Log(
                $"Entity {typeof(T).Name} with ID {id} not found.", 
                LogLevel.Warning);
        }
        
        return entity;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(bool includeDeleted = false)
    {
        var query = GetQueryable(includeDeleted);
        return await query.ToListAsync();
    }

    public virtual async Task AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await _dbSet.AddAsync(entity);
        
        LoggerSingleton.GetInstance().Log(
            $"Added new {typeof(T).Name} entity.", 
            LogLevel.Info);
    }

    public virtual async Task UpdateAsync(T entity)
    {
        var existingEntity = await _dbSet.FindAsync(entity.Id);
        if (existingEntity == null)
        {
            throw new EntityNotFoundException(typeof(T).Name, entity.Id);
        }

        entity.ModifiedAt = DateTime.UtcNow;
        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        
        LoggerSingleton.GetInstance().Log(
            $"Updated {typeof(T).Name} with ID {entity.Id}.", 
            LogLevel.Info);
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(T).Name, id);
        }

        _dbSet.Remove(entity);
        
        LoggerSingleton.GetInstance().Log(
            $"Deleted {typeof(T).Name} with ID {id}.", 
            LogLevel.Info);
    }

    public virtual async Task SoftDeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(T).Name, id);
        }

        entity.DeletedAt = DateTime.UtcNow;
        entity.ModifiedAt = DateTime.UtcNow;
        
        LoggerSingleton.GetInstance().Log(
            $"Soft deleted {typeof(T).Name} with ID {id}.", 
            LogLevel.Info);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    protected IQueryable<T> GetQueryable(bool includeDeleted = false)
    {
        if (includeDeleted)
        {
            return _dbSet.IgnoreQueryFilters();
        }
        return _dbSet.AsQueryable();
    }
}
