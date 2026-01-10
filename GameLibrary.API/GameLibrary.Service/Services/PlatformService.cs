using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;
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

        public async Task UpdatePlatformAsync(int id, UpdatePlatformDto dto)
        {
            if (id < 0)
                throw new ArgumentException("Invalid id");
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentException("Platform name is required");

            var platform = new Platform
            {
                Id = id,
                Name = dto.Name,
                Manufacturer = dto.Manufacturer,
                ReleaseYear = dto.ReleaseYear,
            };

            await _platformDomain.UpdatePlatformAsync(platform);
        }

        public async Task DeletePlatformAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid platform ID");

            await _platformDomain.DeletePlatformAsync(id);
        }

        public async Task<IEnumerable<PlatformDto>> SP_GetPlatformsPaginatedAsync(int pageNumber, int pageSize)
        {
            var platforms = await _platformDomain.SP_GetPlatformsPaginatedAsync(pageNumber, pageSize);
            return platforms.Select(p => p.ToDto());
        }

        public async Task<IEnumerable<PlatformDto>> SP_SearchPlatformsByNameAsync(string nameTerm)
        {
            if (string.IsNullOrWhiteSpace(nameTerm))
                throw new ArgumentException("Search term cannot be empty");

            var platforms = await _platformDomain.SP_SearchPlatformsByNameAsync(nameTerm);
            return platforms.Select(p => p.ToDto());
        }

        public async Task<IEnumerable<PlatformDto>> SP_GetPlatformsByReleaseYearAsync(int releaseYear)
        {
            if (releaseYear < 1950 || releaseYear > DateTime.Now.Year + 5)
                throw new ArgumentException("Invalid release year");

            var platforms = await _platformDomain.SP_GetPlatformsByReleaseYearAsync(releaseYear);
            return platforms.Select(p => p.ToDto());
        }

    }
}
