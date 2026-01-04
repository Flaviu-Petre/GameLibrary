using Microsoft.EntityFrameworkCore;
using Xunit;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repository;
using GameLibrary.Entity.Entities;

namespace GameLibrary.Test.Repository
{
    public class GameRepositoryTests
    {
        private GameLibraryDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<GameLibraryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new GameLibraryDbContext(options);
        }

        [Fact]
        public async Task GetByTitleAsync_ShouldReturnGame_WhenExists()
        {
            using var context = GetContext();
            var game = new Game { Title = "Half-Life 3", ReleaseDate = DateTime.Now };
            context.Games.Add(game);
            await context.SaveChangesAsync();

            var repo = new GameRepository(context);

            var result = await repo.GetByTitleAsync("Half-Life 3");

            Assert.NotNull(result);
            Assert.Equal("Half-Life 3", result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldIncludeRelations()
        {
            using var context = GetContext();

            var dev = new Developer { Name = "Valve" };
            var pub = new Publisher { Name = "Valve" };
            var plat = new Platform { Name = "PC" };
            var genre = new Genre { Name = "FPS" };

            var game = new Game
            {
                Title = "Portal",
                ReleaseDate = DateTime.Now,
                Developer = dev,
                Publisher = pub,
                Platform = plat
            };
            game.Genres.Add(genre);

            context.Games.Add(game);
            await context.SaveChangesAsync();

            var repo = new GameRepository(context);

            var result = await repo.GetByIdAsync(game.Id);

            Assert.NotNull(result);
            Assert.NotNull(result.Developer);
            Assert.Equal("Valve", result.Developer.Name);
            Assert.NotNull(result.Publisher);
            Assert.NotNull(result.Platform);
            Assert.Single(result.Genres);
            Assert.Equal("FPS", result.Genres.First().Name);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllGamesWithRelations()
        {
            using var context = GetContext();
            context.Games.Add(new Game { Title = "Game 1", Developer = new Developer { Name = "Dev1" } });
            context.Games.Add(new Game { Title = "Game 2", Developer = new Developer { Name = "Dev2" } });
            await context.SaveChangesAsync();

            var repo = new GameRepository(context);

            var result = await repo.GetAllAsync();

            Assert.Equal(2, result.Count());
            Assert.NotNull(result.First().Developer);
        }
    }
}