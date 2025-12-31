using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Entity.Entities;
using GameLibrary.Service.Dtos.User;
using GameLibrary.Service.Mapping;
using GameLibrary.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Service.Services
{
    public class UserService(IUserDomain userDomain) : IUserService
    {
        private readonly IUserDomain _userDomain = userDomain;
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userDomain.GetAllUsersAsync();
            return users.Select(u => u.ToDto());
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var user = await _userDomain.GetUserByIdAsync(id);
            return user?.ToDto();
        }

        public async Task<UserDto?> GetUserByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            var user = await _userDomain.GetUserByUsernameAsync(username);
            return user?.ToDto();
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email
            };

            await _userDomain.CreateUserAsync(user, dto.Password);

            return user.ToDto();
        }

        public async Task DeleteUserAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid user ID");

            await _userDomain.DeleteUserAsync(id);
        }

        public async Task<bool> ValidatePasswordAsync(string username, string password)
        {
            return await _userDomain.ValidatePasswordAsync(username, password);
        }

        public async Task UpdateUserProfileAsync(int id, UpdateUserDto dto)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid user ID");

            if(string.IsNullOrEmpty(dto.Username) && string.IsNullOrEmpty(dto.Email))
                throw new ArgumentException("At least one field (Username or Email) must be provided for update");

            await _userDomain.UpdateUserProfileAsync(id, dto.Username, dto.Email);
        }

        public async Task ChangePasswordAsync(int id, ChangePasswordDto dto)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid user ID");

            await _userDomain.ChangePasswordAsync(id, dto.CurrentPassword, dto.NewPassword);
        }
    }
}
