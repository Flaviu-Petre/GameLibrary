using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Repository.Interface;

namespace GameLibrary.Domain.Domains
{
    public class PublisherDomain(IPublisherRepository publisherRepository) : IPublisherDomain
    {
        private readonly IPublisherRepository _publisherRepository = publisherRepository;

        public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
        {
            return await _publisherRepository.GetAllAsync();
        }
        public async Task<Publisher?> GetPublisherByIdAsync(int id)
        {
            return await _publisherRepository.GetByIdAsync(id);
        }
        public async Task<Publisher?> GetPublisherByNameAsync(string name)
        {
            return await _publisherRepository.GetByNameAsync(name);
        }
        public async Task AddPublisherAsync(Publisher publisher)
        {
            if (string.IsNullOrEmpty(publisher.Name))
                throw new ArgumentException("Publisher name cannot be empty");
            await _publisherRepository.AddAsync(publisher);
            await _publisherRepository.SaveChangesAsync();
        }
        public async Task UpdatePublisherAsync(Publisher publisher)
        {
            await _publisherRepository.UpdateAsync(publisher);
            await _publisherRepository.SaveChangesAsync();
        }
        public async Task DeletePublisherAsync(int id)
        {
            try
            {
                await _publisherRepository.SoftDeleteAsync(id);
            }
            catch(Exception ex)
            {
                throw new ArgumentException($"Error deleting publisher with ID {id}: {ex.Message}");
            }
            await _publisherRepository.SaveChangesAsync();
        }   
    }
}
