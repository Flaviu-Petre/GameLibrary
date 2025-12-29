using GameLibrary.Service.Dtos.Developer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Service.Services.Interface
{
    public interface IDeveloperService
    {
        Task<IEnumerable<DeveloperDto>> GetAllDevelopersAsync();
        Task<DeveloperDto?> GetDeveloperByIdAsync(int id);
        Task<DeveloperDto> CreateDeveloperAsync(CreateDeveloperDto dto);
        Task<DeveloperDto> GetDeveloperByNameAsync(string name);
        Task DeleteDeveloperAsync(int id);

    }
}
