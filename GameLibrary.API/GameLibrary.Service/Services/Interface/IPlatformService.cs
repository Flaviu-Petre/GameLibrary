using GameLibrary.Service.Dtos.Developer;
using GameLibrary.Service.Dtos.Platform;

namespace GameLibrary.Service.Services.Interface
{
    public interface IPlatformService
    {
        Task<IEnumerable<PlatformDto>> GetAllPlatformsAsync();
        Task<PlatformDto?> GetPlatformByIdAsync(int id);
        Task<PlatformDto> CreatePlatformAsync(CreatePlatformDto dto);
        Task<PlatformDto?> GetPlatformByNameAsync(string name);
        Task UpdatePlatformAsync(int id, UpdatePlatformDto dto);
        Task DeletePlatformAsync(int id);
        Task<IEnumerable<PlatformDto>> SP_GetPlatformsPaginatedAsync(int pageNumber, int pageSize);
        Task<IEnumerable<PlatformDto>> SP_SearchPlatformsByNameAsync(string nameTerm);
        Task<IEnumerable<PlatformDto>> SP_GetPlatformsByReleaseYearAsync(int releaseYear);
    }
}
