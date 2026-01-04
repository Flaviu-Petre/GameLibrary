using Moq;
using Xunit;
using GameLibrary.Domain.Domains;
using GameLibrary.Repository.Repository.Interface;
using GameLibrary.Entity.Entities;

namespace GameLibrary.Test.Domain
{
    public class GameDomainTests
    {
        private readonly Mock<IGameRepository> _mockGameRepo;
        private readonly Mock<IGenreRepository> _mockGenreRepo;
        private readonly Mock<IDeveloperRepository> _mockDevRepo;
        private readonly Mock<IPublisherRepository> _mockPubRepo;
        private readonly Mock<IPlatformRepository> _mockPlatRepo;
        private readonly GameDomain _domain;

        public GameDomainTests()
        {
            _mockGameRepo = new Mock<IGameRepository>();
            _mockGenreRepo = new Mock<IGenreRepository>();
            _mockDevRepo = new Mock<IDeveloperRepository>();
            _mockPubRepo = new Mock<IPublisherRepository>();
            _mockPlatRepo = new Mock<IPlatformRepository>();

            _domain = new GameDomain(
                _mockGameRepo.Object,
                _mockGenreRepo.Object,
                _mockDevRepo.Object,
                _mockPlatRepo.Object,
                _mockPubRepo.Object
            );
        }

        [Fact]
        public async Task CreateGameAsync_ShouldThrow_WhenForeignKeysAreInvalid()
        {
            var game = new Game { Title = "Test Game" };

            await Assert.ThrowsAsync<ArgumentException>(() =>
                _domain.CreateGameAsync(game, 1, 1, 0, new List<int> { 1 }));
        }

        [Fact]
        public async Task CreateGameAsync_ShouldCallAdd_WhenAllEntitiesExist()
        {
            var game = new Game { Title = "New Game" };
            var genreIds = new List<int> { 10 };

            _mockDevRepo.Setup(r => r.GetByIdAsync(1, false)).ReturnsAsync(new Developer());
            _mockPubRepo.Setup(r => r.GetByIdAsync(2, false)).ReturnsAsync(new Publisher());
            _mockPlatRepo.Setup(r => r.GetByIdAsync(3, false)).ReturnsAsync(new Platform());
            _mockGenreRepo.Setup(r => r.GetByIdAsync(10, false)).ReturnsAsync(new Genre());

            await _domain.CreateGameAsync(game, 1, 2, 3, genreIds);

            _mockGameRepo.Verify(r => r.AddAsync(game), Times.Once);
            Assert.Single(game.Genres);
            _mockGameRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateGameAsync_ShouldThrow_WhenGenreNotFound()
        {
            var game = new Game { Title = "Fail Game" };

            _mockDevRepo.Setup(r => r.GetByIdAsync(1, false)).ReturnsAsync(new Developer());
            _mockPubRepo.Setup(r => r.GetByIdAsync(1, false)).ReturnsAsync(new Publisher());
            _mockPlatRepo.Setup(r => r.GetByIdAsync(1, false)).ReturnsAsync(new Platform());

            _mockGenreRepo.Setup(r => r.GetByIdAsync(99, false)).ReturnsAsync((Genre?)null);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                _domain.CreateGameAsync(game, 1, 1, 1, new List<int> { 99 }));
        }

        [Fact]
        public async Task UpdateGameAsync_ShouldThrow_WhenGameNotFound()
        {
            _mockGameRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>(), false)).ReturnsAsync((Game?)null);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                _domain.UpdateGameAsync(1, new Game(), new List<int>()));
        }

        [Fact]
        public async Task UpdateGameAsync_ShouldUpdateRelations_WhenChanged()
        {
            var existingGame = new Game
            {
                Id = 1,
                Title = "Old Title",
                DeveloperId = 10,
                Developer = new Developer { Id = 10 }
            };

            var updateInfo = new Game
            {
                Title = "New Title",
                DeveloperId = 20,
                PublisherId = 5,
                PlatformId = 5
            };
            var newGenreIds = new List<int> { 100 };

            _mockGameRepo.Setup(r => r.GetByIdAsync(1, false)).ReturnsAsync(existingGame);

            _mockDevRepo.Setup(r => r.GetByIdAsync(20, false)).ReturnsAsync(new Developer { Id = 20 });
            _mockPubRepo.Setup(r => r.GetByIdAsync(5, false)).ReturnsAsync(new Publisher());
            _mockPlatRepo.Setup(r => r.GetByIdAsync(5, false)).ReturnsAsync(new Platform());
            _mockGenreRepo.Setup(r => r.GetByIdAsync(100, false)).ReturnsAsync(new Genre());

            await _domain.UpdateGameAsync(1, updateInfo, newGenreIds);

            Assert.Equal("New Title", existingGame.Title);
            Assert.Equal(20, existingGame.Developer.Id);
            Assert.Single(existingGame.Genres);
            _mockGameRepo.Verify(r => r.UpdateAsync(existingGame), Times.Once);
        }

        [Fact]
        public async Task DeleteGameAsync_ShouldCallSoftDelete()
        {
            await _domain.DeleteGameAsync(1);
            _mockGameRepo.Verify(r => r.SoftDeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetGameByTitleAsync_ShouldCallRepo()
        {
            await _domain.GetGameByTitleAsync("Zelda");
            _mockGameRepo.Verify(r => r.GetByTitleAsync("Zelda"), Times.Once);
        }
    }
}