using Microsoft.EntityFrameworkCore;
using Xunit;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repository;
using GameLibrary.Entity.Entities;

namespace GameLibrary.Test.Repository
{
    public class UserRepositoryTests
    {
        private GameLibraryDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<GameLibraryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new GameLibraryDbContext(options);
        }

        [Fact]
        public async Task EmailExistsAsync_ShouldReturnTrue_WhenExists()
        {
            using var context = GetContext();
            context.Users.Add(new User { Username = "user1", Email = "test@example.com" });
            await context.SaveChangesAsync();
            var repo = new UserRepository(context);

            var result = await repo.EmailExistsAsync("test@example.com");

            Assert.True(result);
        }

        [Fact]
        public async Task UsernameExistsAsync_ShouldReturnTrue_WhenExists()
        {
            using var context = GetContext();
            context.Users.Add(new User { Username = "gamer123", Email = "g@example.com" });
            await context.SaveChangesAsync();
            var repo = new UserRepository(context);

            var result = await repo.UsernameExistsAsync("gamer123");

            Assert.True(result);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnUser()
        {
            using var context = GetContext();
            context.Users.Add(new User { Username = "u", Email = "found@example.com" });
            await context.SaveChangesAsync();
            var repo = new UserRepository(context);

            var result = await repo.GetByEmailAsync("found@example.com");

            Assert.NotNull(result);
            Assert.Equal("u", result.Username);
        }

        [Fact]
        public async Task GetByUsernameAsync_ShouldReturnUser()
        {
            using var context = GetContext();
            context.Users.Add(new User { Username = "uniqueUser", Email = "u@example.com" });
            await context.SaveChangesAsync();
            var repo = new UserRepository(context);

            var result = await repo.GetByUsernameAsync("uniqueUser");

            Assert.NotNull(result);
            Assert.Equal("u@example.com", result.Email);
        }
    }
}