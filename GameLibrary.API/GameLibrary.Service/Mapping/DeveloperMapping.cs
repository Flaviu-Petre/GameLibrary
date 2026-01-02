using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Entity.Entities;
using GameLibrary.Service.Dtos.Developer;

namespace GameLibrary.Service.Mapping
{
    public static class DeveloperMapping
    {
        public static DeveloperDto ToDto(this Developer developer)
        {
            return new DeveloperDto
            {
                Id = developer.Id,
                Name = developer.Name,
                Country = developer.Country,
                GamesCount = developer.Games?.Count ?? 0
            };
        }

        public static Developer ToEntity(this CreateDeveloperDto dto)
        {
            return new Developer
            {
                Name = dto.Name,
                Country = dto.Country,
                FoundedDate = dto.FoundedDate
            };
        }

        public static Developer ToEntity(this UpdateDeveloperDto dto)
        {
            return new Developer
            {
                Name = dto.Name,
                Country = dto.Country,
                FoundedDate = dto.FoundedDate
            };
        }

    }
}
