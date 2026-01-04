using Moq;
using Xunit;
using GameLibrary.Service.Services;
using GameLibrary.Service.Dtos.Publisher;
using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;

namespace GameLibrary.Test.Service
{
    public class PublisherServiceTests
    {
        private readonly Mock<IPublisherDomain> _mockDomain;
        private readonly PublisherService _service;

        public PublisherServiceTests()
        {
            _mockDomain = new Mock<IPublisherDomain>();
            _service = new PublisherService(_mockDomain.Object);
        }

        [Fact]
        public async Task CreatePublisherAsync_ShouldThrowException_WhenNameIsEmpty()
        {
            var dto = new CreatePublisherDto { Name = "" };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreatePublisherAsync(dto));
        }

        [Fact]
        public async Task CreatePublisherAsync_ShouldReturnDto_OnSuccess()
        {
            var dto = new CreatePublisherDto { Name = "New Pub", Website = "www.pub.com" };

            var result = await _service.CreatePublisherAsync(dto);

            Assert.Equal("New Pub", result.Name);
            _mockDomain.Verify(d => d.AddPublisherAsync(It.IsAny<Publisher>()), Times.Once);
        }

        [Fact]
        public async Task GetPublisherByIdAsync_ShouldReturnDto_WhenPublisherExists()
        {
            int testId = 1;
            var publisher = new Publisher { Id = testId, Name = "Test Pub", Website = "test.com" };
            _mockDomain.Setup(d => d.GetPublisherByIdAsync(testId)).ReturnsAsync(publisher);

            var result = await _service.GetPublisherByIdAsync(testId);

            Assert.NotNull(result);
            Assert.Equal("Test Pub", result.Name);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task GetPublisherByIdAsync_ShouldReturnNull_WhenIdIsInvalid(int id)
        {
            var result = await _service.GetPublisherByIdAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllPublishersAsync_ShouldReturnDtoList()
        { 
            var publishers = new List<Publisher> { new Publisher { Id = 1, Name = "Pub 1" } };
            _mockDomain.Setup(d => d.GetAllPublishersAsync()).ReturnsAsync(publishers);

            var result = await _service.GetAllPublishersAsync();

            Assert.Single(result);
            Assert.Equal("Pub 1", result.First().Name);
        }

        [Fact]
        public async Task GetPublisherByNameAsync_ShouldThrow_WhenNameIsEmpty()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetPublisherByNameAsync(""));
        }

        [Fact]
        public async Task UpdatePublisherAsync_ShouldThrow_WhenNameIsNull()
        {
            var dto = new UpdatePublisherDto { Name = null };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.UpdatePublisherAsync(dto));
        }

        [Fact]
        public async Task UpdatePublisherAsync_ShouldThrow_WhenWebsiteIsNull()
        {
            var dto = new UpdatePublisherDto { Name = "Valid", Website = null };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.UpdatePublisherAsync(dto));
        }

        [Fact]
        public async Task UpdatePublisherAsync_ShouldCallDomain_WhenValid()
        {
            var dto = new UpdatePublisherDto { Id = 1, Name = "Updated Name", Website = "site.com" };

            await _service.UpdatePublisherAsync(dto);

            _mockDomain.Verify(d => d.UpdatePublisherAsync(It.Is<Publisher>(p => p.Id == 1 && p.Name == "Updated Name")), Times.Once);
        }

        [Fact]
        public async Task DeletePublisherAsync_ShouldThrow_WhenIdIsInvalid()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.DeletePublisherAsync(-5));
        }

        [Fact]
        public async Task DeletePublisherAsync_ShouldCallDomain_WhenIdIsValid()
        {
            await _service.DeletePublisherAsync(1);

            _mockDomain.Verify(d => d.DeletePublisherAsync(1), Times.Once);
        }
    }
}