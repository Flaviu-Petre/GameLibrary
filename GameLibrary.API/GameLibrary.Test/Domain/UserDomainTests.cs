using Moq;
using Xunit;
using GameLibrary.Domain.Domains;
using GameLibrary.Repository.Repository.Interface;
using GameLibrary.Entity.Entities;

namespace GameLibrary.Test.Domain
{
    public class UserDomainTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly UserDomain _domain;

        public UserDomainTests()
        {
            _mockRepo = new Mock<IUserRepository>();
            _domain = new UserDomain(_mockRepo.Object);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldThrow_WhenPasswordIsShort()
        {
            var user = new User { Username = "valid", Email = "valid@mail.com" };
            await Assert.ThrowsAsync<ArgumentException>(() => _domain.CreateUserAsync(user, "short"));
        }

        [Fact]
        public async Task CreateUserAsync_ShouldThrow_WhenUsernameExists()
        {
            var user = new User { Username = "existing", Email = "new@mail.com" };
            _mockRepo.Setup(r => r.UsernameExistsAsync("existing")).ReturnsAsync(true);

            await Assert.ThrowsAsync<ArgumentException>(() => _domain.CreateUserAsync(user, "password123"));
        }

        [Fact]
        public async Task CreateUserAsync_ShouldCallAdd_WhenValid()
        {
            var user = new User { Username = "new", Email = "new@mail.com" };
            _mockRepo.Setup(r => r.UsernameExistsAsync("new")).ReturnsAsync(false);
            _mockRepo.Setup(r => r.EmailExistsAsync("new@mail.com")).ReturnsAsync(false);

            await _domain.CreateUserAsync(user, "password123");

            _mockRepo.Verify(r => r.AddAsync(user), Times.Once);
            Assert.NotNull(user.PasswordHash);
        }

        [Fact]
        public async Task UpdateUserProfileAsync_ShouldThrow_WhenUsernameTakenByOtherUser()
        {
            var currentUser = new User { Id = 1, Username = "oldName", Email = "old@mail.com" };
            var otherUser = new User { Id = 2, Username = "takenName", Email = "other@mail.com" };

            _mockRepo.Setup(r => r.GetByIdAsync(1, false)).ReturnsAsync(currentUser);
            _mockRepo.Setup(r => r.GetByUsernameAsync("takenName")).ReturnsAsync(otherUser);

            await Assert.ThrowsAsync<ArgumentException>(() => _domain.UpdateUserProfileAsync(1, "takenName", "old@mail.com"));
        }

        [Fact]
        public async Task UpdateUserProfileAsync_ShouldUpdate_WhenValid()
        {
            var currentUser = new User { Id = 1, Username = "oldName", Email = "old@mail.com" };
            _mockRepo.Setup(r => r.GetByIdAsync(1, false)).ReturnsAsync(currentUser);

            await _domain.UpdateUserProfileAsync(1, "newName", "new@mail.com");

            Assert.Equal("newName", currentUser.Username);
            _mockRepo.Verify(r => r.UpdateAsync(currentUser), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldCallSoftDelete()
        {
            await _domain.DeleteUserAsync(1);
            _mockRepo.Verify(r => r.SoftDeleteAsync(1), Times.Once);
        }
        [Fact]
        public async Task ChangePasswordAsync_ShouldThrow_WhenUserNotFound()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>(), false)).ReturnsAsync((User?)null);
            await Assert.ThrowsAsync<ArgumentException>(() => _domain.ChangePasswordAsync(1, "old", "new"));
        }
    }
}