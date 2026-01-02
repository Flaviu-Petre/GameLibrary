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
        Task CreateGameAsync(Game genre);
        Task<IEnumerable<Game?>> GetAllGamesAsync();
    }
}
