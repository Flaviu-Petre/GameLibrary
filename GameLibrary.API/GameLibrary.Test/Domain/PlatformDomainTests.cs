using GameLibrary.Domain.Domains;
using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Repository.Interface;
using Moq;
using Xunit;

namespace GameLibrary.Test.Domain
{
    public class PlatformDomainTests
    {
        private readonly Mock<IPlatformRepository> _mockRepo;
        private readonly PlatformDomain _domain;

        public PlatformDomainTests()
        {
            _mockRepo = new Mock<IPlatformRepository>();
            _domain = new PlatformDomain(_mockRepo.Object);
        }

        [Fact]
        public async Task AddPlatformAsync_ShouldCallRepo_WhenValid()
        {
            // arrange
            var platform = new Platform { Name = "PC" };

            // act
            await _domain.AddPlatformAsync(platform);

            // assert
            _mockRepo.Verify(r => r.AddAsync(platform), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddPlatformAsync_ShouldThrowException_WhenNameIsNull()
        {
            // arrange
            var platform = new Platform { Name = null };

            // act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => _domain.AddPlatformAsync(platform));
        }

        [Fact]
        public async Task GetPlatformByIdAsync_ShouldCallRepo()
        {
            // act
            await _domain.GetPlatformByIdAsync(1);

            // assert
            _mockRepo.Verify(r => r.GetByIdAsync(1, false), Times.Once);
        }

        [Fact]
        public async Task GetAllPlatformsAsync_ShouldCallRepo()
        {
            // act
            await _domain.GetAllPlatformsAsync();

            // assert
            _mockRepo.Verify(r => r.GetAllAsync(false), Times.Once);
        }

        [Fact]
        public async Task UpdatePlatformAsync_ShouldCallRepo()
        {
            // arrange
            var platform = new Platform { Id = 1, Name = "Updated" };

            // act
            await _domain.UpdatePlatformAsync(platform);

            // assert
            _mockRepo.Verify(r => r.UpdateAsync(platform), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeletePlatformAsync_ShouldCallSoftDelete()
        {
            // act
            await _domain.DeletePlatformAsync(1);

            // assert
            _mockRepo.Verify(r => r.SoftDeleteAsync(1), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetPlatformByNameAsync_ShouldCallRepo()
        {
            // act
            await _domain.GetPlatformByNameAsync("Xbox");

            // assert
            _mockRepo.Verify(r => r.GetByNameAsync("Xbox"), Times.Once);
        }
    }
}