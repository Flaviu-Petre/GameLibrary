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

    }
}
