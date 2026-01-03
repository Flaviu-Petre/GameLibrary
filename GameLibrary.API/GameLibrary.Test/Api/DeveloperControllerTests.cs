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
    }
}
