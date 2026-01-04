using GameLibrary.Domain.Domains;
using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;
using GameLibrary.Service.Dtos.Game;
using GameLibrary.Service.Mapping;
using GameLibrary.Service.Services.Interface;

namespace GameLibrary.Service.Services
{
    public class GameService(IGameDomain gameDomain) : IGameService
    {
        private readonly IGameDomain _gameDomain = gameDomain;

        public async Task<GameDto> CreateGameAsync(CreateGameDto payload)
        {
            if (string.IsNullOrWhiteSpace(payload.Title))
            {
                throw new ArgumentException("Game title cannot be null or empty.");
            }
            if (string.IsNullOrWhiteSpace(payload.Description))
            {
                throw new ArgumentException("Game description cannot be null or empty.");
            }
            if (!payload.DeveloperId.HasValue || payload.DeveloperId.Value < 0)
            {
                throw new ArgumentException("Game developer index is invalid.");
            }
            if (!payload.PublisherId.HasValue || payload.PublisherId.Value < 0)
            {
                throw new ArgumentException("Game publisher index is invalid.");
            }
            if (!payload.PlatformId.HasValue || payload.PlatformId.Value < 0)
            {
                throw new ArgumentException("Game platform index is invalid.");
            }
            if (payload.GenreIds.Count == 0)
            {
                throw new ArgumentException("Game generes indexes cannot be null or empty.");
            }
            if (payload.GenreIds.Any(id => id < 0))
            {
                throw new ArgumentException("One or more game genre indexes are invalid.");
            }

            Game entity = payload.ToEntity();

            await _gameDomain.CreateGameAsync(
                entity,
                payload.DeveloperId.Value,
                payload.PublisherId.Value,
                payload.PlatformId.Value,
                payload.GenreIds
            );

            return entity.ToDto();
        }
        public async Task<IEnumerable<GameDto>> GetAllGamesAsync()
        {
            var games = await _gameDomain.GetAllGamesAsync();
            return games.Select(g => g.ToDto());
        }
        public async Task DeleteGameByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid game ID.");
            }

            await _gameDomain.DeleteGameAsync(id);
        }
        public async Task UpdateGameAsync(int id, UpdateGameDto dto)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid game ID.");

            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("Title required.");

            var gameInfo = dto.ToEntity();

            await _gameDomain.UpdateGameAsync(id, gameInfo, dto.GenreIds);
        }
        public async Task<GameDto?> GetGameByIdAsync(int id)
        {
            var game = await _gameDomain.GetGameByIdAsync(id);

            if (game == null)
            {
                return null;
            }

            return game.ToDto();
        }
        public async Task<GameDto?> GetGameByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            var game = await _gameDomain.GetGameByTitleAsync(name);

            if (game == null)
            {
                return null;
            }

            return game.ToDto();
        }
    }
}
