using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Domain.Domains;
using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Service.Dtos.Developer;
using GameLibrary.Service.Dtos.Publisher;
using GameLibrary.Service.Mapping;
using GameLibrary.Service.Services.Interface;

namespace GameLibrary.Service.Services
{
    public class PublisherService (IPublisherDomain publisherDomain) : IPublisherService
    {
        private readonly IPublisherDomain _publisherDomain = publisherDomain;

        public async Task<IEnumerable<PublisherDto>> GetAllPublishersAsync()
        {
            var developers = await _publisherDomain.GetAllPublishersAsync();
            return developers.Select(d => d.ToDto());
        }

        public async Task<PublisherDto?> GetPublisherByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var developer = await _publisherDomain.GetPublisherByIdAsync(id);
            return developer?.ToDto();
        }

        public async Task<PublisherDto> CreatePublisherAsync(CreatePublisherDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
                throw new ArgumentException("Developer name is required");

            var developer = dto.ToEntity();
            await _publisherDomain.AddPublisherAsync(developer);
            return developer.ToDto();
        }

        public async Task<PublisherDto?> GetPublisherByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Developer name is required");

            var developer = await _publisherDomain.GetPublisherByNameAsync(name);
            return developer?.ToDto();
        }


        public async Task DeletePublisherAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid developer ID");

            await _publisherDomain.DeletePublisherAsync(id);
        }

        public Task UpdatePublisherAsync(UpdatePublisherDto dto)
        {
            if (dto.Name == null)
                throw new ArgumentException("Publisher name is required");

            if (dto.Website == null)
                throw new ArgumentException("Country is required");

            var publisher = dto.ToEntity();

            return _publisherDomain.UpdatePublisherAsync(publisher);
        }
    }
}
