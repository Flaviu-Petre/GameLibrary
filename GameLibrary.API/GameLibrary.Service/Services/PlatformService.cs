using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Service.Dtos.Platform;
using GameLibrary.Service.Mapping;
using GameLibrary.Service.Services.Interface;

namespace GameLibrary.Service.Services
{
    public class PlatformService(IPlatformDomain platformDomain): IPlatformService
    {
        private readonly IPlatformDomain _platformDomain = platformDomain;

        public async Task<IEnumerable<PlatformDto>> GetAllPlatformsAsync()
        {
            var platforms = await _platformDomain.GetAllPlatformsAsync();
            return platforms.Select(d => d.ToDto()); ;
        }

        public async Task<PlatformDto?> GetPlatformByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var platform = await _platformDomain.GetPlatformByIdAsync(id);
            return platform?.ToDto();
        }

        public async Task<PlatformDto> CreatePlatformAsync(CreatePlatformDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentException("Platform name is required");

            var platform = dto.ToEntity();
            await _platformDomain.AddPlatformAsync(platform);
            return platform.ToDto();
        }

        public async Task<PlatformDto?> GetPlatformByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Platform name is required");
            var platform = await _platformDomain.GetPlatformByNameAsync(name);
            return platform?.ToDto();
        }

        public async Task DeletePlatformAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid platform ID");

            await _platformDomain.DeletePlatformAsync(id);
        }
    }
}
