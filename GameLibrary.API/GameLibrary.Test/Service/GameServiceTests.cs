using Moq;
using Xunit;
using GameLibrary.Service.Services;
using GameLibrary.Service.Dtos.Game;
using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;

namespace GameLibrary.Test.Service
{
    public class GameServiceTests
    {
        private readonly Mock<IGameDomain> _mockDomain;
        private readonly GameService _service;

        public GameServiceTests()
        {
            _mockDomain = new Mock<IGameDomain>();
            _service = new GameService(_mockDomain.Object);
        }

        [Fact]
        public async Task CreateGameAsync_ShouldThrow_WhenTitleInvalid()
        {
            var dto = new CreateGameDto { Title = "" };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateGameAsync(dto));
        }

        [Fact]
        public async Task CreateGameAsync_ShouldThrow_WhenGenreListEmpty()
        {
            var dto = new CreateGameDto
            {
                Title = "Valid",
                Description = "Desc",
                DeveloperId = 1,
                PublisherId = 1,
                PlatformId = 1,
                GenreIds = new List<int>()
            };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateGameAsync(dto));
        }

        [Fact]
        public async Task CreateGameAsync_ShouldCallDomain_WhenValid()
        {
            var dto = new CreateGameDto
            {
                Title = "RPG",
                Description = "Cool Game",
                DeveloperId = 1,
                PublisherId = 2,
                PlatformId = 3,
                GenreIds = new List<int> { 10, 20 }
            };

            var result = await _service.CreateGameAsync(dto);

            Assert.Equal("RPG", result.Title);
            _mockDomain.Verify(d => d.CreateGameAsync(
                It.IsAny<Game>(), 1, 2, 3, dto.GenreIds), Times.Once);
        }

        [Fact]
        public async Task UpdateGameAsync_ShouldThrow_WhenIdInvalid()
        {
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.UpdateGameAsync(0, new UpdateGameDto()));
        }

        [Fact]
        public async Task GetGameByIdAsync_ShouldReturnDto_WhenFound()
        {
            var game = new Game { Title = "Test" };
            _mockDomain.Setup(d => d.GetGameByIdAsync(1)).ReturnsAsync(game);

            var result = await _service.GetGameByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Title);
        }

        [Fact]
        public async Task GetGameByNameAsync_ShouldThrow_WhenNameEmpty()
        {
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.GetGameByNameAsync(""));
        }
    }
}