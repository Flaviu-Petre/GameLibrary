using Moq;
using Xunit;
using GameLibrary.Service.Services;
using GameLibrary.Service.Dtos.User;
using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;

namespace GameLibrary.Test.Service
{
    public class UserServiceTests
    {
        private readonly Mock<IUserDomain> _mockDomain;
        private readonly UserService _service;

        public UserServiceTests()
        {
            _mockDomain = new Mock<IUserDomain>();
            _service = new UserService(_mockDomain.Object);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenIdInvalid()
        {
            var result = await _service.GetUserByIdAsync(0);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnDto_WhenFound()
        {
            var user = new User { Id = 1, Username = "Test" };
            _mockDomain.Setup(d => d.GetUserByIdAsync(1)).ReturnsAsync(user);

            var result = await _service.GetUserByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Username);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldCallDomainAndReturnDto()
        {
            var dto = new CreateUserDto { Username = "New", Email = "new@mail.com", Password = "pass" };

            var result = await _service.CreateUserAsync(dto);

            Assert.Equal("New", result.Username);
            _mockDomain.Verify(d => d.CreateUserAsync(It.IsAny<User>(), "pass"), Times.Once);
        }

        [Fact]
        public async Task UpdateUserProfileAsync_ShouldThrow_WhenBothFieldsEmpty()
        {
            var dto = new UpdateUserDto { Username = "", Email = "" };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.UpdateUserProfileAsync(1, dto));
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldThrow_WhenIdInvalid()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.DeleteUserAsync(-1));
        }

        [Fact]
        public async Task ValidatePasswordAsync_ShouldReturnDomainResult()
        {
            _mockDomain.Setup(d => d.ValidatePasswordAsync("user", "pass")).ReturnsAsync(true);
            var result = await _service.ValidatePasswordAsync("user", "pass");
            Assert.True(result);
        }
    }
}