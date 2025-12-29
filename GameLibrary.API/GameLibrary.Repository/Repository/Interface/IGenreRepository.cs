using GameLibrary.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Repository.Repository.Interface
{
    public interface IGenreRepository
    {
        Task<Genre?> GetByNameAsync(string name);
    }
}
