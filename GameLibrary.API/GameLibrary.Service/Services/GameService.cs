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
            if (payload.DeveloperId < 0)
            {
                throw new ArgumentException("Game developer index is invalid.");
            }
            if (payload.PublisherId < 0)
            {
                throw new ArgumentException("Game publisher index is invalid.");
            }
            if (payload.PlatformId < 0)
            {
                throw new ArgumentException("Game platform index is invalid.");
            }
            if (payload.GenresId.Count == 0)
            {
                throw new ArgumentException("Game generes indexes cannot be null or empty.");
            }
            if (payload.GenresId.Any(id => id < 0))
            {
                throw new ArgumentException("One or more game genre indexes are invalid.");
            }

            Game entity = payload.ToEntity();

            await _gameDomain.CreateGameAsync(entity);

            return entity.ToDto();
        }

        public Task<IEnumerable<GameDto>> GetAllGamesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
