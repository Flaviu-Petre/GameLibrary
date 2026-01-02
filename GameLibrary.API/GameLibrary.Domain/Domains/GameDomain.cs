using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Repository.Interface;

namespace GameLibrary.Domain.Domains
{
    public class GameDomain(IGameRepository gameRepository,
                            IGenreRepository genreRepository,
                            IDeveloperRepository developerRepository,
                            IPlatformRepository platformRepository,
                            IPublisherRepository publisherRepository) 
        : IGameDomain
    {
        private readonly IGameRepository _gameRepository = gameRepository;
        private readonly IGenreRepository _genreRepository = genreRepository;
        private readonly IDeveloperRepository _developerRepository = developerRepository;
        private readonly IPlatformRepository _platformRepository = platformRepository;
        private readonly IPublisherRepository _publisherRepository = publisherRepository;

        public async Task CreateGameAsync(Game game, int developerId, int publisherId, int platformId, ICollection<int> genreIds)
        {
            game.DeveloperId = developerId;
            game.PublisherId = publisherId;
            game.PlatformId = platformId;

            if (string.IsNullOrEmpty(game.Title))
                throw new ArgumentException("Game title cannot be empty");

            if (game.DeveloperId == null || game.DeveloperId <= 0)
                throw new ArgumentException("Game must have a developer");
            if (game.PublisherId == null || game.PublisherId <= 0)
                throw new ArgumentException("Game must have a publisher");
            if (game.PlatformId == null || game.PlatformId <= 0)
                throw new ArgumentException("Game must have a platform");

            if (genreIds == null || !genreIds.Any())
                throw new ArgumentException("Game must have at least one genre");

            game.Developer = await _developerRepository.GetByIdAsync(developerId)
                ?? throw new ArgumentException($"Developer with ID {developerId} does not exist.");

            game.Publisher = await _publisherRepository.GetByIdAsync(publisherId)
                ?? throw new ArgumentException($"Publisher with ID {publisherId} does not exist.");

            game.Platform = await _platformRepository.GetByIdAsync(platformId)
                ?? throw new ArgumentException($"Platform with ID {platformId} does not exist.");

            foreach (var id in genreIds)
            {
                var genre = await _genreRepository.GetByIdAsync(id);
                if (genre != null)
                {
                    game.Genres.Add(genre);
                }
                else
                {
                    throw new ArgumentException($"Genre with ID {id} does not exist.");
                }
            }

            await _gameRepository.AddAsync(game);
            await _gameRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game?>> GetAllGamesAsync()
        {
            return await _gameRepository.GetAllAsync();
        }
    }
}
