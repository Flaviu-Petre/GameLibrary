using Microsoft.EntityFrameworkCore;
using Xunit;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repository;
using GameLibrary.Entity.Entities;

namespace GameLibrary.Test.Repository
{
    public class DeveloperRepositoryTests
    {
        private GameLibraryDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<GameLibraryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new GameLibraryDbContext(options);
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnDeveloper()
        {
            // Arrange
            using var context = GetContext();
            context.Developers.Add(new Developer { Name = "Ubisoft", Country = "France" });
            await context.SaveChangesAsync();

            var repo = new DeveloperRepository(context);

            // Act
            var result = await repo.GetByNameAsync("Ubisoft");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("France", result.Country);
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnNull_WhenNameDoesNotExist()
        {
            // Arrange
            using var context = GetContext();
            var repo = new DeveloperRepository(context);

            // Act
            var result = await repo.GetByNameAsync("NonExistentDeveloper");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldIncludeGames_WhenDeveloperExists()
        {
            // Arrange
            using var context = GetContext();
            var developer = new Developer { Name = "FromSoftware", Country = "Japan" };

            developer.Games.Add(new Game
            {
                Title = "Elden Ring",
                ReleaseDate = DateTime.UtcNow,
                Description = "Open world RPG"
            });

            context.Developers.Add(developer);
            await context.SaveChangesAsync();

            var repo = new DeveloperRepository(context);

            // Act
            var result = await repo.GetByIdAsync(developer.Id);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Games);
            Assert.Single(result.Games);
            Assert.Equal("Elden Ring", result.Games.First().Title);
        }
    }
}
