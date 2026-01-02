using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;
using GameLibrary.Service.Dtos.Genre;
using GameLibrary.Service.Services;
using Moq;
using Xunit;

namespace GameLibrary.Test.Service
{
    public class GenreServiceTests
    {
        private readonly Mock<IGenreDomain> _mockDomain;
        private readonly GenreService _service;

        public GenreServiceTests()
        {
            _mockDomain = new Mock<IGenreDomain>();
            _service = new GenreService(_mockDomain.Object);
        }

        [Fact]
        public async Task GetGenreByIdAsync_ShouldReturnDto_WhenGenreExists()
        {
            // arrange
            int testId = 1;
            var fakeGenre = new Genre { Id = testId, Name = "Horror", Description = "Games that will cause fear"};

            _mockDomain.Setup(repo => repo.GetGenreByIdAsync(testId))
                       .ReturnsAsync(fakeGenre);

            // act
            var result = await _service.GetGenreByIdAsync(testId);

            // assert
            Assert.NotNull(result);
            Assert.Equal("Horror", result.Name);
            Assert.Equal("Games that will cause fear", result.Description);
            _mockDomain.Verify(m => m.GetGenreByIdAsync(testId), Times.Once);
        }

        [Fact]
        public async Task CreateGenreAsync_ShouldThrowException_WhenNameIsEmpty()
        {
            // arrange
            var dto = new CreateGenreDto { Name = "" };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.CreateGenreAsync(dto));
        }
    }
}
