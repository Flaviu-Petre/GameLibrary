using GameLibrary.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Domain.Domains.Interface
{
    public interface IGameDomain
    {
        Task CreateGameAsync(Game game, int developerId, int publisherId, int platformId, ICollection<int> genreIds);
        Task<IEnumerable<Game?>> GetAllGamesAsync();
    }
}
