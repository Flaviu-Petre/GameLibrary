using GameLibrary.Service.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Service.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<UserDto?> GetUserByUsernameAsync(string username);
        Task<UserDto> CreateUserAsync(CreateUserDto dto);
        Task UpdateUserProfileAsync(int id, UpdateUserDto dto);
        Task ChangePasswordAsync(int id, ChangePasswordDto dto);
        Task DeleteUserAsync(int id);
        Task<bool> ValidatePasswordAsync(string username, string password);
    }
}
