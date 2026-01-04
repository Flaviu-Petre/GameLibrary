using Microsoft.EntityFrameworkCore;
using Xunit;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repository;
using GameLibrary.Entity.Entities;

namespace GameLibrary.Test.Repository
{
    public class PublisherRepositoryTests
    {
        private GameLibraryDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<GameLibraryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new GameLibraryDbContext(options);
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnPublisher()
        {
            using var context = GetContext();
            context.Publishers.Add(new Publisher { Name = "EA Games", Country = "USA", Website = "ea.com" });
            await context.SaveChangesAsync();

            var repo = new PublisherRepository(context);

            var result = await repo.GetByNameAsync("EA Games");

            Assert.NotNull(result);
            Assert.Equal("USA", result.Country);
            Assert.Equal("ea.com", result.Website);
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnNull_WhenNameDoesNotExist()
        {
            using var context = GetContext();
            var repo = new PublisherRepository(context);

            var result = await repo.GetByNameAsync("NonExistentPublisher");

            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddPublisherToDatabase()
        {
            using var context = GetContext();
            var repo = new PublisherRepository(context);
            var publisher = new Publisher { Name = "Sega", Country = "Japan" };

            await repo.AddAsync(publisher);
            await repo.SaveChangesAsync();

            var result = await context.Publishers.FirstOrDefaultAsync(p => p.Name == "Sega");
            Assert.NotNull(result);
            Assert.Equal("Japan", result.Country);
        }
    }
}