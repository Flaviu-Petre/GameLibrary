using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Repository;
using GameLibrary.Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Domain.Domains
{
    public class GameDomain(IGameRepository gameRepository) : IGameDomain
    {
        private readonly IGameRepository _gameRepository = gameRepository;

        public async Task CreateGameAsync(Game game)
        {
            if (string.IsNullOrEmpty(game.Title))
                throw new ArgumentException("Game title cannot be empty");
            if(game.DeveloperId == null)
                throw new ArgumentException("Game must have a developer");
            if(game.PublisherId == null)
                throw new ArgumentException("Game must have a publisher");
            if(game.PlatformId == null)
                throw new ArgumentException("Game must have a platform");
            if(game.GenresId == null || !game.GenresId.Any())
                throw new ArgumentException("Game must have at least one genre");

            await _gameRepository.AddAsync(game);
            await _gameRepository.SaveChangesAsync();
        }

        public Task<IEnumerable<Game?>> GetAllGamesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
