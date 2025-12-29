using GameLibrary.Entity.Entities;

namespace GameLibrary.Repository.Repositories.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id, bool includeDeleted = false);
    Task<IEnumerable<T>> GetAllAsync(bool includeDeleted = false);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);
    Task SaveChangesAsync();
}
