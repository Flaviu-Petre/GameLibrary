using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Repository.Interface;

namespace GameLibrary.Domain.Domains
{
    public class GenreDomain(IGenreRepository genreRepository) : IGenreDomain
    {
        private readonly IGenreRepository _genreRepository = genreRepository;

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _genreRepository.GetAllAsync();
        }

        public async Task<Genre?> GetGenreByIdAsync(int id)
        {
            return await _genreRepository.GetByIdAsync(id);
        }

        public async Task<Genre?> GetGenreByNameAsync(string name)
        {
            return await _genreRepository.GetByNameAsync(name);
        }

        public async Task CreateGenreAsync(Genre genre)
        {
            if (string.IsNullOrEmpty(genre.Name))
                throw new ArgumentException("Genre name cannot be empty");

            await _genreRepository.AddAsync(genre);
            await _genreRepository.SaveChangesAsync();
        }

        public async Task UpdateGenreAsync(Genre genre)
        {
            await _genreRepository.UpdateAsync(genre);
            await _genreRepository.SaveChangesAsync();
        }

        public async Task DeleteGenreByIdAsync(int id)
        {
            await _genreRepository.SoftDeleteAsync(id);
            await _genreRepository.SaveChangesAsync();
        }
    }
}
