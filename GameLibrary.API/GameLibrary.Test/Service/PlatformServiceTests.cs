using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;
using GameLibrary.Service.Dtos.Platform;
using GameLibrary.Service.Services;
using Moq;
using Xunit;

namespace GameLibrary.Test.Service
{
    public class PlatformServiceTests
    {
        private readonly Mock<IPlatformDomain> _mockDomain;
        private readonly PlatformService _service;

        public PlatformServiceTests()
        {
            _mockDomain = new Mock<IPlatformDomain>();
            _service = new PlatformService(_mockDomain.Object);
        }

        [Fact]
        public async Task CreatePlatformAsync_ShouldReturnDto_OnSuccess()
        {
            // arrange
            var dto = new CreatePlatformDto { Name = "PS5", Manufacturer = "Sony" };

            // act
            var result = await _service.CreatePlatformAsync(dto);

            // assert
            Assert.Equal("PS5", result.Name);
            _mockDomain.Verify(d => d.AddPlatformAsync(It.IsAny<Platform>()), Times.Once);
        }

        [Fact]
        public async Task CreatePlatformAsync_ShouldThrow_WhenNameIsEmpty()
        {
            // arrange
            var dto = new CreatePlatformDto { Name = "" };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreatePlatformAsync(dto));
        }

        [Fact]
        public async Task GetPlatformByIdAsync_ShouldReturnDto_WhenExists()
        {
            // arrange
            var platform = new Platform { Id = 1, Name = "PC" };
            _mockDomain.Setup(d => d.GetPlatformByIdAsync(1)).ReturnsAsync(platform);

            // act
            var result = await _service.GetPlatformByIdAsync(1);

            // assert
            Assert.NotNull(result);
            Assert.Equal("PC", result.Name);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task GetPlatformByIdAsync_ShouldReturnNull_ForInvalidId(int id)
        {
            // act
            var result = await _service.GetPlatformByIdAsync(id);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllPlatformsAsync_ShouldReturnList()
        {
            // arrange
            var list = new List<Platform> { new Platform { Name = "P1" }, new Platform { Name = "P2" } };
            _mockDomain.Setup(d => d.GetAllPlatformsAsync()).ReturnsAsync(list);

            // act
            var result = await _service.GetAllPlatformsAsync();

            // assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetPlatformByNameAsync_ShouldThrow_WhenNameIsNull()
        {
            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetPlatformByNameAsync(null));
        }

        [Fact]
        public async Task UpdatePlatformAsync_ShouldCallDomain_WhenValid()
        {
            // arrange
            var dto = new UpdatePlatformDto { Name = "Updated" };

            // act
            await _service.UpdatePlatformAsync(1, dto);

            // assert
            _mockDomain.Verify(d => d.UpdatePlatformAsync(It.Is<Platform>(p => p.Id == 1 && p.Name == "Updated")), Times.Once);
        }

        [Fact]
        public async Task UpdatePlatformAsync_ShouldThrow_WhenIdIsNegative()
        {
            // arrange
            var dto = new UpdatePlatformDto { Name = "Valid" };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.UpdatePlatformAsync(-1, dto));
        }

        [Fact]
        public async Task UpdatePlatformAsync_ShouldThrow_WhenNameIsEmpty()
        {
            // arrange
            var dto = new UpdatePlatformDto { Name = "" };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.UpdatePlatformAsync(1, dto));
        }

        [Fact]
        public async Task DeletePlatformAsync_ShouldCallDomain_WhenValid()
        {
            // act
            await _service.DeletePlatformAsync(1);

            // assert
            _mockDomain.Verify(d => d.DeletePlatformAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeletePlatformAsync_ShouldThrow_WhenIdIsInvalid()
        {
            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.DeletePlatformAsync(0));
        }

        [Fact]
        public async Task GetPlatformByNameAsync_ReturnsDto_OnSuccess()
        {
            // arrange
            var platform = new Platform { Name = "Steam" };
            _mockDomain.Setup(d => d.GetPlatformByNameAsync("Steam")).ReturnsAsync(platform);

            // act
            var result = await _service.GetPlatformByNameAsync("Steam");

            // assert
            Assert.Equal("Steam", result.Name);
        }
    }
}