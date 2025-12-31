using GameLibrary.Entity.Entities;
using GameLibrary.Service.Dtos.Developer;

namespace GameLibrary.Service.Services.Interface
{
    public interface IDeveloperService
    {
        Task<IEnumerable<DeveloperDto>> GetAllDevelopersAsync();
        Task<DeveloperDto?> GetDeveloperByIdAsync(int id);
        Task<DeveloperDto> CreateDeveloperAsync(CreateDeveloperDto dto);
        Task<DeveloperDto> GetDeveloperByNameAsync(string name);
        Task DeleteDeveloperAsync(int id);
        Task UpdateDeveloperAsync(int id, UpdateDeveloperDto dto);
        Task<IEnumerable<DeveloperDto>> SP_GetDevelopersByCountryAsync(string country);
        Task<IEnumerable<DeveloperDto>> SP_GetDevelopersPaginatedAsync(int pageNumber, int pageSize);

    }
}
