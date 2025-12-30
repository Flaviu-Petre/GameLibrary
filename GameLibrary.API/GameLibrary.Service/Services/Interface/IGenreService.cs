using GameLibrary.Service.Dtos.Genre;

namespace GameLibrary.Service.Services.Interface
{
    public interface IGenreService
    {
        Task<GenreDto> CreateGenreAsync(CreateGenreDto dto);
        Task<IEnumerable<GenreDto>> GetAllGenresAsync();
        Task<GenreDto> GetGenreByIdAsync(int id);
        Task<GenreDto> GetGenreByNameAsync(string name);
        Task DeleteGenreByIdAsync(int id);
    }
}
