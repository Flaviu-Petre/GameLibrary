using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GameLibrary.Test.Repository
{
    public class PlatformRepositoryTests
    {
        private GameLibraryDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<GameLibraryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new GameLibraryDbContext(options);
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnCorrectPlatform()
        {
            // arrange
            using var context = GetContext();
            context.Platforms.Add(new Platform { Name = "PC", Manufacturer = "Various" });
            await context.SaveChangesAsync();
            var repo = new PlatformRepository(context);

            // act
            var result = await repo.GetByNameAsync("PC");

            // assert
            Assert.NotNull(result);
            Assert.Equal("Various", result.Manufacturer);
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnNull_WhenNameDoesNotExist()
        {
            // arrange
            using var context = GetContext();
            var repo = new PlatformRepository(context);

            // act
            var result = await repo.GetByNameAsync("NonExistent");

            // assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldIncludeGames_WhenPlatformExists()
        {
            // arrange
            using var context = GetContext();
            var platform = new Platform { Name = "PS5", Manufacturer = "Sony" };
            platform.Games.Add(new Game { Title = "Test Game", ReleaseDate = DateTime.UtcNow });
            context.Platforms.Add(platform);
            await context.SaveChangesAsync();
            var repo = new PlatformRepository(context);

            // act
            var result = await repo.GetByIdAsync(platform.Id);

            // assert
            Assert.NotNull(result);
            Assert.Single(result.Games);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnOnlyActivePlatforms()
        {
            // arrange
            using var context = GetContext();
            context.Platforms.AddRange(
                new Platform { Name = "Active" },
                new Platform { Name = "Deleted", DeletedAt = DateTime.UtcNow }
            );
            await context.SaveChangesAsync();
            var repo = new PlatformRepository(context);

            // act
            var result = await repo.GetAllAsync();

            // assert
            Assert.Single(result);
            Assert.Equal("Active", result.First().Name);
        }

        [Fact]
        public async Task SoftDeleteAsync_ShouldSetDeletedAtTimestamp()
        {
            // arrange
            using var context = GetContext();
            var platform = new Platform { Name = "Xbox" };
            context.Platforms.Add(platform);
            await context.SaveChangesAsync();
            var repo = new PlatformRepository(context);

            // act
            await repo.SoftDeleteAsync(platform.Id);
            await repo.SaveChangesAsync();

            // assert
            var deleted = await context.Platforms.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == platform.Id);
            Assert.NotNull(deleted.DeletedAt);
        }

        [Fact]
        public async Task AddAsync_ShouldAssignNewId()
        {
            // arrange
            using var context = GetContext();
            var repo = new PlatformRepository(context);
            var platform = new Platform { Name = "Switch" };

            // act
            await repo.AddAsync(platform);
            await repo.SaveChangesAsync();

            // assert
            Assert.True(platform.Id > 0);
        }

        [Fact]
        public async Task UpdateAsync_ShouldModifyFields()
        {
            // arrange
            using var context = GetContext();
            var platform = new Platform { Name = "Old Name" };
            context.Platforms.Add(platform);
            await context.SaveChangesAsync();
            var repo = new PlatformRepository(context);

            // act
            platform.Name = "New Name";
            await repo.UpdateAsync(platform);
            await repo.SaveChangesAsync();

            // assert
            var updated = await context.Platforms.FindAsync(platform.Id);
            Assert.Equal("New Name", updated.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            // arrange
            using var context = GetContext();
            var repo = new PlatformRepository(context);

            // act
            var result = await repo.GetByIdAsync(999);

            // assert
            Assert.Null(result);
        }
    }
}