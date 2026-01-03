using Moq;
using Xunit;
using GameLibrary.Domain.Domains;
using GameLibrary.Repository.Repository.Interface;
using GameLibrary.Entity.Entities;

namespace GameLibrary.Test.Domain
{
    public class DeveloperDomainTests
    {
        private readonly Mock<IDeveloperRepository> _mockRepo;
        private readonly DeveloperDomain _domain;

        public DeveloperDomainTests()
        {
            _mockRepo = new Mock<IDeveloperRepository>();
            _domain = new DeveloperDomain(_mockRepo.Object);
        }

        [Fact]
        public async Task AddDeveloperAsync_ShouldCallRepo_WhenValid()
        {
            // Arrange
            var dev = new Developer { Name = "Valid Name" };

            // Act
            await _domain.AddDeveloperAsync(dev);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(dev), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task AddDeveloperAsync_ShouldThrowException_WhenNameIsInvalid(string? name)
        {
            // Arrange
            var dev = new Developer { Name = name };

            // Act & Assert 
            await Assert.ThrowsAsync<ArgumentException>(() => _domain.AddDeveloperAsync(dev));
        }

        [Fact]
        public async Task GetAllDevelopersAsync_ShouldReturnCollection()
        {
            // Arrange
            var developers = new List<Developer> { new Developer { Name = "Dev 1" } };
            _mockRepo.Setup(r => r.GetAllAsync(false)).ReturnsAsync(developers);

            // Act
            var result = await _domain.GetAllDevelopersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            _mockRepo.Verify(r => r.GetAllAsync(false), Times.Once);
        }

        [Fact]
        public async Task GetDeveloperByIdAsync_ShouldReturnDeveloper()
        {
            // Arrange
            var developer = new Developer { Id = 1, Name = "Dev 1" };
            _mockRepo.Setup(r => r.GetByIdAsync(1, false)).ReturnsAsync(developer);

            // Act
            var result = await _domain.GetDeveloperByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            _mockRepo.Verify(r => r.GetByIdAsync(1, false), Times.Once);
        }

        [Fact]
        public async Task GetDeveloperByNameAsync_ShouldReturnDeveloper()
        {
            // Arrange
            var developer = new Developer { Name = "Ubisoft" };
            _mockRepo.Setup(r => r.GetByNameAsync("Ubisoft")).ReturnsAsync(developer);

            // Act
            var result = await _domain.GetDeveloperByNameAsync("Ubisoft");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Ubisoft", result.Name);
            _mockRepo.Verify(r => r.GetByNameAsync("Ubisoft"), Times.Once);
        }

        [Fact]
        public async Task UpdateDeveloperAsync_ShouldCallRepo()
        {
            // Arrange
            var dev = new Developer { Id = 1, Name = "Updated Name" };

            // Act
            await _domain.UpdateDeveloperAsync(dev);

            // Assert
            _mockRepo.Verify(r => r.UpdateAsync(dev), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteDeveloperAsync_ShouldCallSoftDelete()
        {
            // Act
            await _domain.DeleteDeveloperAsync(1);

            // Assert
            _mockRepo.Verify(r => r.SoftDeleteAsync(1), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
