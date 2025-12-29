using GameLibrary.Domain.Domains;
using GameLibrary.Service.Dtos.Developer;
using GameLibrary.Service.Mapping;
using GameLibrary.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Service.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly DeveloperDomain _developerDomain;

        public DeveloperService(DeveloperDomain developerDomain)
        {
            _developerDomain = developerDomain;
        }

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
    }
}
