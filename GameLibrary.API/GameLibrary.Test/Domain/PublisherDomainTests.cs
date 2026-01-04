using Moq;
using Xunit;
using GameLibrary.Domain.Domains;
using GameLibrary.Repository.Repository.Interface;
using GameLibrary.Entity.Entities;

namespace GameLibrary.Test.Domain
{
    public class PublisherDomainTests
    {
        private readonly Mock<IPublisherRepository> _mockRepo;
        private readonly PublisherDomain _domain;

        public PublisherDomainTests()
        {
            _mockRepo = new Mock<IPublisherRepository>();
            _domain = new PublisherDomain(_mockRepo.Object);
        }

        [Fact]
        public async Task AddPublisherAsync_ShouldCallRepo_WhenValid()
        {
            var pub = new Publisher { Name = "Valid Publisher" };

            await _domain.AddPublisherAsync(pub);

            _mockRepo.Verify(r => r.AddAsync(pub), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task AddPublisherAsync_ShouldThrowException_WhenNameIsInvalid(string? name)
        {
            var pub = new Publisher { Name = name };

            await Assert.ThrowsAsync<ArgumentException>(() => _domain.AddPublisherAsync(pub));
        }

        [Fact]
        public async Task GetAllPublishersAsync_ShouldReturnCollection()
        {
            var publishers = new List<Publisher> { new Publisher { Name = "Pub 1" } };
            _mockRepo.Setup(r => r.GetAllAsync(false)).ReturnsAsync(publishers);

            var result = await _domain.GetAllPublishersAsync();

            Assert.NotNull(result);
            Assert.Single(result);
            _mockRepo.Verify(r => r.GetAllAsync(false), Times.Once);
        }

        [Fact]
        public async Task GetPublisherByIdAsync_ShouldReturnPublisher()
        {
            // Arrange
            var publisher = new Publisher { Id = 1, Name = "Pub 1" };
            _mockRepo.Setup(r => r.GetByIdAsync(1, false)).ReturnsAsync(publisher);

            // Act
            var result = await _domain.GetPublisherByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            _mockRepo.Verify(r => r.GetByIdAsync(1, false), Times.Once);
        }

        [Fact]
        public async Task GetPublisherByNameAsync_ShouldReturnPublisher()
        {
            // Arrange
            var publisher = new Publisher { Name = "Activision" };
            _mockRepo.Setup(r => r.GetByNameAsync("Activision")).ReturnsAsync(publisher);

            // Act
            var result = await _domain.GetPublisherByNameAsync("Activision");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Activision", result.Name);
            _mockRepo.Verify(r => r.GetByNameAsync("Activision"), Times.Once);
        }

        [Fact]
        public async Task UpdatePublisherAsync_ShouldCallRepo()
        {
            // Arrange
            var pub = new Publisher { Id = 1, Name = "Updated Name" };

            // Act
            await _domain.UpdatePublisherAsync(pub);

            // Assert
            _mockRepo.Verify(r => r.UpdateAsync(pub), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeletePublisherAsync_ShouldCallSoftDelete()
        {
            // Act
            await _domain.DeletePublisherAsync(1);

            // Assert
            _mockRepo.Verify(r => r.SoftDeleteAsync(1), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeletePublisherAsync_ShouldThrowArgumentException_OnRepoError()
        {
            // Arrange
            _mockRepo.Setup(r => r.SoftDeleteAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _domain.DeletePublisherAsync(1));
        }
    }
}