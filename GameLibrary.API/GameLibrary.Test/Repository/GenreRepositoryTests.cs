using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GameLibrary.Test.Repository
{
    public class GenreRepositoryTests
    {
        private GameLibraryDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<GameLibraryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new GameLibraryDbContext(options);
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnCorrectGenre()
        {
            // arrange
            using var context = GetContext();
            context.Genres.Add(new Genre { Name = "Action", Description = "Fast-paced" });
            await context.SaveChangesAsync();
            var repo = new GenreRepository(context);

            // act
            var result = await repo.GetByNameAsync("Action");

            // assert
            Assert.NotNull(result);
            Assert.Equal("Fast-paced", result.Description);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllNonDeletedGenres()
        {
            // arrange
            using var context = GetContext();
            context.Genres.AddRange(
                new Genre { Name = "Genre1" },
                new Genre { Name = "Genre2", DeletedAt = DateTime.UtcNow }
            );
            await context.SaveChangesAsync();
            var repo = new GenreRepository(context);
            
            // act
            var result = await repo.GetAllAsync();
            
            // assert
            Assert.Single(result);
        }

        [Fact]
        public async Task SoftDeleteAsync_ShouldSetDeletedAtTimestamp()
        {
            // arrange
            using var context = GetContext();
            var genre = new Genre { Name = "DeleteMe", Description = "Desc" };
            context.Genres.Add(genre);
            await context.SaveChangesAsync();
            var repo = new GenreRepository(context);

            // act
            await repo.SoftDeleteAsync(genre.Id);
            await repo.SaveChangesAsync();

            // assert
            var deletedGenre = await context.Genres.IgnoreQueryFilters().FirstOrDefaultAsync(g => g.Id == genre.Id);
            Assert.NotNull(deletedGenre.DeletedAt);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateFieldsCorrectly()
        {
            // arrange
            using var context = GetContext();
            var genre = new Genre { Name = "Old Name", Description = "Old Desc" };
            context.Genres.Add(genre);
            await context.SaveChangesAsync();
            var repo = new GenreRepository(context);

            // act
            genre.Name = "New Name";
            await repo.UpdateAsync(genre);
            await repo.SaveChangesAsync();

            // assert
            var updated = await context.Genres.FindAsync(genre.Id);
            Assert.Equal("New Name", updated.Name);
        }

        [Fact]
        public async Task AddAsync_ShouldAssignNewId()
        {
            // arrange
            using var context = GetContext();
            var repo = new GenreRepository(context);
            var genre = new Genre { Name = "Indie", Description = "Independent" };

            // act
            await repo.AddAsync(genre);
            await repo.SaveChangesAsync();

            // assert
            Assert.True(genre.Id > 0);
        }
    }
}
