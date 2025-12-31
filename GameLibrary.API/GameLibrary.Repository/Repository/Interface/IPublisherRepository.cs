using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Repositories.Interfaces;

namespace GameLibrary.Repository.Repository.Interface
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<Publisher?> GetByNameAsync(string name);
    }
}
