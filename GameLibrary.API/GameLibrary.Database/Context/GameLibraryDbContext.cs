using GameLibrary.Database.Entities;
using GameLibrary.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Database.Context
{
    public class GameLibraryDbContext : DbContext
    {
        public GameLibraryDbContext(DbContextOptions<GameLibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Platform> Platforms { get; set; }


    }
}
