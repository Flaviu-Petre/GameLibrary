using GameLibrary.Repository.Repository.Interface;
using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;

namespace GameLibrary.Domain.Domains
{
    public class PlatformDomain(IPlatformRepository platformRepository) : IPlatformDomain
    {
        private readonly IPlatformRepository _platformRepository = platformRepository;

        public async Task<IEnumerable<Platform>> GetAllPlatformsAsync()
        {
            return await _platformRepository.GetAllAsync();
        }
        public async Task<Platform?> GetPlatformByIdAsync(int id)
        {
            return await _platformRepository.GetByIdAsync(id);
        }
        public async Task<Platform?> GetPlatformByNameAsync(string name)
        {
            return await _platformRepository.GetByNameAsync(name);
        }
        public async Task AddPlatformAsync(Platform platform)
        {
            if (string.IsNullOrEmpty(platform.Name))
                throw new ArgumentException("Platform name cannot be empty");
            await _platformRepository.AddAsync(platform);
            await _platformRepository.SaveChangesAsync();
        }
        public async Task UpdatePlatformAsync(Platform platform)
        {
            await _platformRepository.UpdateAsync(platform);
            await _platformRepository.SaveChangesAsync();
        }
        public async Task DeletePlatformAsync(int id)
        {
            await _platformRepository.SoftDeleteAsync(id);
            await _platformRepository.SaveChangesAsync();
        }
    }

}
