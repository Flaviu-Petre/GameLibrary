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
    }
}
