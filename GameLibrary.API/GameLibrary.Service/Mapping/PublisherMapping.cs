using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Entity.Entities;
using GameLibrary.Service.Dtos.Developer;
using GameLibrary.Service.Dtos.Publisher;

namespace GameLibrary.Service.Mapping
{
    public static class PublisherMapping
    {
        public static PublisherDto ToDto(this Publisher publisher)
        {
            return new PublisherDto
            {
                Id = publisher.Id,
                Name = publisher.Name,
                Website = publisher.Website
            };
        }

        public static Publisher ToEntity(this CreatePublisherDto dto)
        {
            return new Publisher
            {
                Name = dto.Name,
                Website = dto.Website
            };
        }

        public static Publisher ToEntity(this UpdatePublisherDto dto)
        {
            return new Publisher
            {
                Id = dto.Id,
                Name = dto.Name,
                Website = dto.Website
            };
        }
    }
}
