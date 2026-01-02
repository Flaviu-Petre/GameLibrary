using GameLibrary.Entity.Entities;
using GameLibrary.Service.Dtos.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Service.Mapping
{
    public static class GenreMapping
    {
        public static GenreDto ToDto(this Genre entity)
        {
            return new GenreDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                GamesCount = entity.Games?.Count ?? 0
            };
        }

        public static Genre ToEntity(this CreateGenreDto dto)
        {
            return new Genre
            {
                Name = dto.Name,
                Description = dto.Description,
            };
        }

        public static Genre ToEntity(this UpdateGenreDto dto)
        {
            return new Genre
            {
                Name = dto.Name,
                Description = dto.Description,
            };
        }
    }
}
