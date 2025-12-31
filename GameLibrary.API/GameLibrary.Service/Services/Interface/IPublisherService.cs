using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Service.Dtos.Developer;
using GameLibrary.Service.Dtos.Publisher;

namespace GameLibrary.Service.Services.Interface
{
    public interface IPublisherService
    {
        Task<IEnumerable<PublisherDto>> GetAllPublishersAsync();
        Task<PublisherDto?> GetPublisherByIdAsync(int id);
        Task<PublisherDto> CreatePublisherAsync(CreatePublisherDto dto);
        Task<PublisherDto> GetPublisherByNameAsync(string name);
        Task DeletePublisherAsync(int id);
        public Task UpdatePublisherAsync(UpdatePublisherDto dto);
    }
}
