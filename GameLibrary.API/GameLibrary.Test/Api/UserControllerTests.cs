using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using GameLibrary.API.Controllers;
using GameLibrary.Service.Services.Interface;
using GameLibrary.Service.Dtos.User;

namespace GameLibrary.Test.Api
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockService;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockService = new Mock<IUserService>();
            _controller = new UserController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithList()
        {
            var list = new List<UserDto> { new UserDto { Id = 1, Username = "User1" } };
            _mockService.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(list);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedList = Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);
            Assert.Single(returnedList);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenUserNull()
        {
            _mockService.Setup(s => s.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync((UserDto?)null);
            var result = await _controller.GetById(99);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetByUsername_ReturnsOk_WhenFound()
        {
            var dto = new UserDto { Username = "Gamer" };
            _mockService.Setup(s => s.GetUserByUsernameAsync("Gamer")).ReturnsAsync(dto);

            var result = await _controller.GetByUsername("Gamer");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(dto, okResult.Value);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            var createDto = new CreateUserDto { Username = "New", Password = "Pass" };
            var resultDto = new UserDto { Id = 5, Username = "New" };
            _mockService.Setup(s => s.CreateUserAsync(createDto)).ReturnsAsync(resultDto);

            var result = await _controller.Create(createDto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(5, createdResult.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdateProfile_ReturnsNoContent()
        {
            var dto = new UpdateUserDto { Username = "Updated" };
            var result = await _controller.UpdateProfile(1, dto);
            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(s => s.UpdateUserProfileAsync(1, dto), Times.Once);
        }

        [Fact]
        public async Task ChangePassword_ReturnsNoContent()
        {
            var dto = new ChangePasswordDto { CurrentPassword = "old", NewPassword = "new" };
            var result = await _controller.ChangePassword(1, dto);
            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(s => s.ChangePasswordAsync(1, dto), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
            _mockService.Verify(s => s.DeleteUserAsync(1), Times.Once);
        }
    }
}