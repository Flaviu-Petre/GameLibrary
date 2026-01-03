using GameLibrary.Domain.Domains;
using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Repository.Interface;
using Moq;
using Xunit;

namespace GameLibrary.Test.Domain
{
    public class GenreDomainTests
    {
        private readonly Mock<IGenreRepository> _mockRepo;
        private readonly GenreDomain _domain;

        public GenreDomainTests()
        {
            _mockRepo = new Mock<IGenreRepository>();
            _domain = new GenreDomain(_mockRepo.Object);
        }

        [Fact]
        public async Task CreateGenreAsync_ShouldCallRepo_WhenValid()
        {
            // arrange
            var genre = new Genre { Name = "Adventure" };

            // act
            await _domain.CreateGenreAsync(genre);

            // assert
            _mockRepo.Verify(r => r.AddAsync(genre), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateGenreAsync_ShouldThrowException_WhenNameIsNull()
        {
            // arrange
            var genre = new Genre { Name = null };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _domain.CreateGenreAsync(genre));
        }

        [Fact]
        public async Task GetGenreByIdAsync_ShouldCallRepo()
        {
            // act
            await _domain.GetGenreByIdAsync(1);

            // assert
            _mockRepo.Verify(r => r.GetByIdAsync(1, false), Times.Once);
        }

        [Fact]
        public async Task GetGenreByNameAsync_ShouldCallRepo()
        {
            // act
            await _domain.GetGenreByNameAsync("Action");

            // assert
            _mockRepo.Verify(r => r.GetByNameAsync("Action"), Times.Once);
        }

        [Fact]
        public async Task UpdateGenreAsync_ShouldCallUpdateAndSave()
        {
            // arrange
            var genre = new Genre { Id = 1, Name = "Racing", Description = "Cars" };

            // act
            await _domain.UpdateGenreAsync(genre);

            // assert
            _mockRepo.Verify(r => r.UpdateAsync(genre), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteGenreByIdAsync_ShouldCallSoftDeleteAndSave()
        {
            // act
            await _domain.DeleteGenreByIdAsync(1);

            // assert
            _mockRepo.Verify(r => r.SoftDeleteAsync(1), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllGenresAsync_ShouldCallRepoGetAll()
        {
            // act
            await _domain.GetAllGenresAsync();

            // assert
            _mockRepo.Verify(r => r.GetAllAsync(false), Times.Once);
        }
    }
}
