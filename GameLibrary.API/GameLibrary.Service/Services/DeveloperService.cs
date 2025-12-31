using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;
using GameLibrary.Service.Dtos.Developer;
using GameLibrary.Service.Mapping;
using GameLibrary.Service.Services.Interface;

namespace GameLibrary.Service.Services
{
    public class DeveloperService(IDeveloperDomain developerDomain) : IDeveloperService
    {
        private readonly IDeveloperDomain _developerDomain = developerDomain;

        public async Task<IEnumerable<DeveloperDto>> GetAllDevelopersAsync()
        {
            var developers = await _developerDomain.GetAllDevelopersAsync();
            return developers.Select(d => d.ToDto());
        }

        public async Task<DeveloperDto?> GetDeveloperByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var developer = await _developerDomain.GetDeveloperByIdAsync(id);
            return developer?.ToDto();
        }

        public async Task<DeveloperDto> CreateDeveloperAsync(CreateDeveloperDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentException("Developer name is required");

            var developer = dto.ToEntity();
            await _developerDomain.AddDeveloperAsync(developer);
            return developer.ToDto();
        }

        public async Task<DeveloperDto?> GetDeveloperByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Developer name is required");

            var developer = await _developerDomain.GetDeveloperByNameAsync(name);
            return developer?.ToDto();
        }


        public async Task DeleteDeveloperAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid developer ID");

            await _developerDomain.DeleteDeveloperAsync(id);
        }

        public async Task<IEnumerable<DeveloperDto>> SP_GetDevelopersByCountryAsync(string country)
        {
            if (string.IsNullOrEmpty(country))
                throw new ArgumentException("Country is required");

            var developers = await _developerDomain.SP_GetDevelopersByCountryAsync(country);
            return developers.Select(d => d.ToDto());
        }

        public async Task UpdateDeveloperAsync(int id, UpdateDeveloperDto dto)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid developer ID");

            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentException("Developer name is required");

            var developer = new Developer
            {
                Id = id,
                Name = dto.Name,
                Country = dto.Country,
                FoundedDate = dto.FoundedDate
            };

            await _developerDomain.UpdateDeveloperAsync(developer);
        }

        public async Task<IEnumerable<DeveloperDto>> SP_GetDevelopersPaginatedAsync(int pageNumber, int pageSize)
        {
            var developers = await _developerDomain.SP_GetDevelopersPaginatedAsync(pageNumber, pageSize);
            return developers.Select(d => d.ToDto());
        }
    }
}
