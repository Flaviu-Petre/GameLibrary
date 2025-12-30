using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Repository.Repository.Interface
{
    public interface IDeveloperRepository : IRepository<Developer>
    {
        Task<Developer?> GetByNameAsync(string name);

        Task<IEnumerable<Developer>> SP_GetDevelopersByCountryAsync(string country);
    }
}
