using GameLibrary.Domain.Domains;
using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;
using GameLibrary.Service.Dtos.Genre;
using GameLibrary.Service.Mapping;
using GameLibrary.Service.Services.Interface;

namespace GameLibrary.Service.Services
{
    public class GenreService(IGenreDomain genreDomain) : IGenreService
    {
        private readonly IGenreDomain _genreDomain = genreDomain;

        public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
        {
            var genres = await _genreDomain.GetAllGenresAsync();
            return genres.Select(g => g.ToDto());
        }

        public async Task<GenreDto?> GetGenreByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var genre = await _genreDomain.GetGenreByIdAsync(id);
            return genre?.ToDto();
        }

        public async Task<GenreDto> CreateGenreAsync(CreateGenreDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentException("Genre name is required");

            var genre = dto.ToEntity();
            await _genreDomain.CreateGenreAsync(genre);
            return genre.ToDto();
        }

        public async Task<GenreDto?> GetGenreByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Genre name is required");

            var genre = await _genreDomain.GetGenreByNameAsync(name);
            return genre?.ToDto();
        }

        public async Task<GenreDto> UpdateGenreAsync(int id, UpdateGenreDto dto)
        {
            if (id < 0)
                throw new ArgumentException("Invalid id");
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentException("Genre name is required");
            if (string.IsNullOrEmpty(dto.Description))
                throw new ArgumentException("Genre description is required");

            var genre = dto.ToEntity();
            genre.Id = id;
            await _genreDomain.UpdateGenreAsync(genre);
            return genre.ToDto();
        }

        public async Task DeleteGenreByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid Genre ID");

            await _genreDomain.DeleteGenreByIdAsync(id);
        }
    }
}
