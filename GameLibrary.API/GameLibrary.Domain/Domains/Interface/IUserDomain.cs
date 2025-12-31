using GameLibrary.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Domain.Domains.Interface
{
    public interface IUserDomain
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByEmailAsync(string email);
        Task CreateUserAsync(User user, string password);
        Task UpdateUserProfileAsync(int id, string username, string email);
        Task ChangePasswordAsync(int id, string currentPassword, string newPassword);
        Task DeleteUserAsync(int id);
        Task<bool> ValidatePasswordAsync(string username, string password);
    }
}
