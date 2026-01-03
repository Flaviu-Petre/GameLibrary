using GameLibrary.API.Controllers;
using GameLibrary.Service.Dtos.Platform;
using GameLibrary.Service.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GameLibrary.Test.Api
{
    public class PlatformControllerTests
    {
        private readonly Mock<IPlatformService> _mockService;
        private readonly PlatformController _controller;

        public PlatformControllerTests()
        {
            _mockService = new Mock<IPlatformService>();
            _controller = new PlatformController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllPlatforms_ReturnsOk()
        {
            // arrange
            _mockService.Setup(s => s.GetAllPlatformsAsync()).ReturnsAsync(new List<PlatformDto>());

            // act
            var result = await _controller.GetAllPlatforms();

            // assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetPlatformById_ReturnsOk_WhenFound()
        {
            // arrange
            var dto = new PlatformDto { Id = 1, Name = "PC" };
            _mockService.Setup(s => s.GetPlatformByIdAsync(1)).ReturnsAsync(dto);

            // act
            var result = await _controller.GetPlatformById(1);

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(dto, okResult.Value);
        }

        [Fact]
        public async Task GetPlatformById_ReturnsNotFound_WhenMissing()
        {
            // arrange
            _mockService.Setup(s => s.GetPlatformByIdAsync(99)).ReturnsAsync((PlatformDto)null);

            // act
            var result = await _controller.GetPlatformById(99);

            // assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetPlatformByName_ReturnsOk_WhenFound()
        {
            // arrange
            var dto = new PlatformDto { Name = "Xbox" };
            _mockService.Setup(s => s.GetPlatformByNameAsync("Xbox")).ReturnsAsync(dto);

            // act
            var result = await _controller.GetPlatformByName("Xbox");

            // assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreatePlatform_ReturnsCreatedAtAction()
        {
            // arrange
            var createDto = new CreatePlatformDto { Name = "New" };
            var resultDto = new PlatformDto { Id = 10, Name = "New" };
            _mockService.Setup(s => s.CreatePlatformAsync(createDto)).ReturnsAsync(resultDto);

            // act
            var result = await _controller.CreatePlatform(createDto);

            // assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(10, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdatePlatform_ReturnsNoContent()
        {
            // arrange
            var dto = new UpdatePlatformDto { Name = "Updated" };

            // act
            var result = await _controller.UpdatePlatform(1, dto);

            // assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeletePlatform_ReturnsNoContent()
        {
            // act
            var result = await _controller.DeletePlatform(1);

            // assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetPlatformByName_ReturnsNotFound_WhenMissing()
        {
            // arrange
            _mockService.Setup(s => s.GetPlatformByNameAsync("Unknown")).ReturnsAsync((PlatformDto)null);

            // act
            var result = await _controller.GetPlatformByName("Unknown");

            // assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}