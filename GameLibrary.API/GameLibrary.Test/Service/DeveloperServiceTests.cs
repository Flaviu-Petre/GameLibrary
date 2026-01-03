using Moq;
using Xunit;
using GameLibrary.Service.Services;
using GameLibrary.Service.Dtos.Developer;
using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;

namespace GameLibrary.Test.Service
{
    public class DeveloperServiceTests
    {
        private readonly Mock<IDeveloperDomain> _mockDomain;
        private readonly DeveloperService _service;

        public DeveloperServiceTests()
        {
            _mockDomain = new Mock<IDeveloperDomain>();
            _service = new DeveloperService(_mockDomain.Object);
        }

        [Fact]
        public async Task CreateDeveloperAsync_ShouldThrowException_WhenNameIsEmpty()
        {
            // Arrange
            var dto = new CreateDeveloperDto { Name = "" };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateDeveloperAsync(dto));
        }

        [Fact]
        public async Task GetDeveloperByIdAsync_ShouldReturnDto_WhenDeveloperExists()
        {
            // Arrange
            int testId = 1;
            var developer = new Developer { Id = testId, Name = "Test Dev", Country = "Test Country" };
            _mockDomain.Setup(d => d.GetDeveloperByIdAsync(testId)).ReturnsAsync(developer);

            // Act
            var result = await _service.GetDeveloperByIdAsync(testId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Dev", result.Name);
        }

        [Fact]
        public async Task GetAllDevelopersAsync_ShouldReturnDtoList()
        {
            // Arrange
            var developers = new List<Developer> { new Developer { Id = 1, Name = "Dev 1" } };
            _mockDomain.Setup(d => d.GetAllDevelopersAsync()).ReturnsAsync(developers);

            // Act
            var result = await _service.GetAllDevelopersAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Dev 1", result.First().Name);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task GetDeveloperByIdAsync_ShouldReturnNull_WhenIdIsInvalid(int id)
        {
            // Act
            var result = await _service.GetDeveloperByIdAsync(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateDeveloperAsync_ShouldReturnDto_OnSuccess()
        {
            // Arrange
            var dto = new CreateDeveloperDto { Name = "New Dev", Country = "USA" };

            // Act
            var result = await _service.CreateDeveloperAsync(dto);

            // Assert
            Assert.Equal("New Dev", result.Name);
            _mockDomain.Verify(d => d.AddDeveloperAsync(It.IsAny<Developer>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDeveloperAsync_ShouldThrow_WhenIdIsInvalid()
        {
            var dto = new UpdateDeveloperDto { Name = "Valid" };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.UpdateDeveloperAsync(0, dto));
        }

        [Fact]
        public async Task UpdateDeveloperAsync_ShouldCallDomain_WhenValid()
        {
            // Arrange
            var dto = new UpdateDeveloperDto { Name = "Updated Name" };

            // Act
            await _service.UpdateDeveloperAsync(1, dto);

            // Assert
            _mockDomain.Verify(d => d.UpdateDeveloperAsync(It.Is<Developer>(dev => dev.Id == 1 && dev.Name == "Updated Name")), Times.Once);
        }

        [Fact]
        public async Task DeleteDeveloperAsync_ShouldThrow_WhenIdIsInvalid()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.DeleteDeveloperAsync(-5));
        }

        [Fact]
        public async Task DeleteDeveloperAsync_ShouldCallDomain_WhenIdIsValid()
        {
            // Act
            await _service.DeleteDeveloperAsync(1);

            // Assert
            _mockDomain.Verify(d => d.DeleteDeveloperAsync(1), Times.Once);
        }

        [Fact]
        public async Task SP_GetDevelopersByCountryAsync_ShouldThrow_WhenCountryIsEmpty()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.SP_GetDevelopersByCountryAsync(""));
        }

        [Fact]
        public async Task SP_GetDevelopersByCountryAsync_ShouldReturnList()
        {
            // Arrange
            var list = new List<Developer> { new Developer { Name = "Romanian Dev", Country = "Romania" } };
            _mockDomain.Setup(d => d.SP_GetDevelopersByCountryAsync("Romania")).ReturnsAsync(list);

            // Act
            var result = await _service.SP_GetDevelopersByCountryAsync("Romania");

            // Assert
            Assert.Single(result);
            Assert.Equal("Romania", result.First().Country);
        }
    }
}
