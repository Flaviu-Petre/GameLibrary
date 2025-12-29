using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Repository.Interface;

namespace GameLibrary.Domain.Domains
{
    public class DeveloperDomain(IDeveloperRepository developerRepository) : IDeveloperDomain
    {
        private readonly IDeveloperRepository _developerRepository = developerRepository;

        public async Task<IEnumerable<Developer>> GetAllDevelopersAsync()
        {
            return await _developerRepository.GetAllAsync();
        }

        public async Task<Developer?> GetDeveloperByIdAsync(int id)
        {
            return await _developerRepository.GetByIdAsync(id);
        }

        public async Task<Developer?> GetDeveloperByNameAsync(string name)
        {
            return await _developerRepository.GetByNameAsync(name);
        }

        public async Task AddDeveloperAsync(Developer developer)
        {
            if (string.IsNullOrEmpty(developer.Name))
                throw new ArgumentException("Developer name cannot be empty");

            await _developerRepository.AddAsync(developer);
            await _developerRepository.SaveChangesAsync();
        }

        public async Task UpdateDeveloperAsync(Developer developer)
        {
            await _developerRepository.UpdateAsync(developer);
            await _developerRepository.SaveChangesAsync();
        }

        public async Task DeleteDeveloperAsync(int id)
        {
            await _developerRepository.SoftDeleteAsync(id);
            await _developerRepository.SaveChangesAsync();
        }
    }
}
