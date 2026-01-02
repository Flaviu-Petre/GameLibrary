using GameLibrary.Entity.Entities;
using GameLibrary.Service.Dtos.Game;
using GameLibrary.Service.Dtos.Genre;

namespace GameLibrary.Service.Mapping
{
    public static class GameMapping
    {
        public static Game ToEntity(this CreateGameDto dto)
        {
            return new Game
            {
                Title = dto.Title,
                Description = dto.Description,
                ReleaseDate = dto.ReleaseDate
            };
        }

        public static GameDto ToDto(this Game entity)
        {
            return new GameDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                ReleaseDate = entity.ReleaseDate,
                Developer = entity.Developer?.Name,
                Publisher = entity.Publisher?.Name,
                Platform = entity.Platform?.Name,
                UserCount = entity.Users?.Count.ToString(),
                Genres = entity.Genres.Select(g => g.Name ?? string.Empty).ToList()
            };
        }
    }
}
