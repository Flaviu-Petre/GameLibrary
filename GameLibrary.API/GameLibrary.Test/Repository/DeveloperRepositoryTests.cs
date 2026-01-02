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
    }
}
