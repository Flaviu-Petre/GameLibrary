using GameLibrary.Entity.Entities;
using System.Collections.Generic;

namespace GameLibrary.Domain.Domains.Interface
{
    public interface IGenreDomain
    {
        Task CreateGenreAsync(Genre genre);
        Task<IEnumerable<Genre?>> GetAllGenresAsync();
        Task<Genre?> GetGenreByIdAsync(int id);
        Task<Genre?> GetGenreByNameAsync(string name);
        Task UpdateGenreAsync(Genre genre);
        Task DeleteGenreByIdAsync(int id);
    }
}
