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
    }
}
