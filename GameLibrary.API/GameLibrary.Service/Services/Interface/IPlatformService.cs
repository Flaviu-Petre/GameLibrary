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
    }
}
