using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using GameLibrary.API.Controllers;
using GameLibrary.Service.Services.Interface;
using GameLibrary.Service.Dtos.Developer;

namespace GameLibrary.Test.Api
{
    public class DeveloperControllerTests
    {
        private readonly Mock<IDeveloperService> _mockService;
        private readonly DevelopersController _controller;

        public DeveloperControllerTests()
        {
            _mockService = new Mock<IDeveloperService>();
            _controller = new DevelopersController(_mockService.Object);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenDeveloperDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetDeveloperByIdAsync(It.IsAny<int>()))
                        .ReturnsAsync((DeveloperDto)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithList()
        {
            // Arrange
            var list = new List<DeveloperDto> { new DeveloperDto { Id = 1, Name = "Ubisoft" } };
            _mockService.Setup(s => s.GetAllDevelopersAsync()).ReturnsAsync(list);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedList = Assert.IsAssignableFrom<IEnumerable<DeveloperDto>>(okResult.Value);
            Assert.Single(returnedList);
            _mockService.Verify(s => s.GetAllDevelopersAsync(), Times.Once);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenExists()
        {
            // Arrange
            var dto = new DeveloperDto { Id = 1, Name = "Nintendo" };
            _mockService.Setup(s => s.GetDeveloperByIdAsync(1)).ReturnsAsync(dto);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(dto, okResult.Value);
        }

        [Fact]
        public async Task GetByName_ReturnsOk_WhenExists()
        {
            // Arrange
            var dto = new DeveloperDto { Name = "RockStar" };
            _mockService.Setup(s => s.GetDeveloperByNameAsync("RockStar")).ReturnsAsync(dto);

            // Act
            var result = await _controller.GetByName("RockStar");

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(dto, okResult.Value);
        }

        [Fact]
        public async Task GetByName_ReturnsNotFound_WhenDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetDeveloperByNameAsync(It.IsAny<string>()))
                        .ReturnsAsync((DeveloperDto)null);

            // Act
            var result = await _controller.GetByName("Unknown");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task SP_GetByCountry_ReturnsOkList()
        {
            // Arrange
            var list = new List<DeveloperDto> { new DeveloperDto { Country = "Romania" } };
            _mockService.Setup(s => s.SP_GetDevelopersByCountryAsync("Romania")).ReturnsAsync(list);

            // Act
            var result = await _controller.SP_GetByCountry("Romania");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(list, okResult.Value);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            // Arrange
            var createDto = new CreateDeveloperDto { Name = "New Dev" };
            var resultDto = new DeveloperDto { Id = 10, Name = "New Dev" };
            _mockService.Setup(s => s.CreateDeveloperAsync(createDto)).ReturnsAsync(resultDto);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetById", createdAtResult.ActionName);
            Assert.Equal(10, createdAtResult.RouteValues["id"]);
            Assert.Equal(resultDto, createdAtResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNoContent()
        {
            // Arrange
            var updateDto = new UpdateDeveloperDto { Name = "Updated Name" };

            // Act
            var result = await _controller.Update(1, updateDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(s => s.UpdateDeveloperAsync(1, updateDto), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(s => s.DeleteDeveloperAsync(1), Times.Once);
        }

        [Fact]
        public async Task SP_GetPaginated_ReturnsOkList()
        {
            // Arrange
            var list = new List<DeveloperDto> { new DeveloperDto { Name = "Page 1" } };
            _mockService.Setup(s => s.SP_GetDevelopersPaginatedAsync(1, 5)).ReturnsAsync(list);

            // Act
            var result = await _controller.SP_GetPaginated(1, 5);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(list, okResult.Value);
        }
    }
}
