using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using GameLibrary.API.Controllers;
using GameLibrary.Service.Services.Interface;
using GameLibrary.Service.Dtos.Publisher;
using GameLibrary.Service.Dtos.Developer;

namespace GameLibrary.Test.Api
{
    public class PublisherControllerTests
    {
        private readonly Mock<IPublisherService> _mockService;
        private readonly PublishersController _controller;

        public PublisherControllerTests()
        {
            _mockService = new Mock<IPublisherService>();
            _controller = new PublishersController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithList()
        {
            var list = new List<PublisherDto> { new PublisherDto { Id = 1, Name = "Ubisoft" } };
            _mockService.Setup(s => s.GetAllPublishersAsync()).ReturnsAsync(list);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedList = Assert.IsAssignableFrom<IEnumerable<PublisherDto>>(okResult.Value);
            Assert.Single(returnedList);
            _mockService.Verify(s => s.GetAllPublishersAsync(), Times.Once);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenExists()
        {
            var dto = new PublisherDto { Id = 1, Name = "Nintendo" };
            _mockService.Setup(s => s.GetPublisherByIdAsync(1)).ReturnsAsync(dto);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(dto, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenPublisherDoesNotExist()
        {
            _mockService.Setup(s => s.GetPublisherByIdAsync(It.IsAny<int>()))
                        .ReturnsAsync((PublisherDto?)null);

            var result = await _controller.GetById(99);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            var createDto = new CreatePublisherDto { Name = "New Pub" };
            var resultDto = new PublisherDto { Id = 10, Name = "New Pub" };
            _mockService.Setup(s => s.CreatePublisherAsync(createDto)).ReturnsAsync(resultDto);

            var result = await _controller.Create(createDto);

            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetById", createdAtResult.ActionName);
            Assert.Equal(10, createdAtResult.RouteValues["id"]);
            Assert.Equal(resultDto, createdAtResult.Value);
        }

        [Fact]
        public async Task GetByName_ReturnsOk_WhenExists()
        {
            var dto = new PublisherDto { Name = "RockStar" };
            _mockService.Setup(s => s.GetPublisherByNameAsync("RockStar")).ReturnsAsync(dto);

            var result = await _controller.GetByName("RockStar");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(dto, okResult.Value);
        }

        [Fact]
        public async Task GetByName_ReturnsNotFound_WhenDoesNotExist()
        {
            _mockService.Setup(s => s.GetPublisherByNameAsync(It.IsAny<string>()))
                        .ReturnsAsync((PublisherDto?)null);

            var result = await _controller.GetByName("Unknown");

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Update_ReturnsNoContent()
        {
            var updateDto = new UpdatePublisherDto { Id = 1, Name = "Updated Name" };

            var result = await _controller.Update(updateDto);

            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(s => s.UpdatePublisherAsync(updateDto), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(s => s.DeletePublisherAsync(1), Times.Once);
        }
    }
}