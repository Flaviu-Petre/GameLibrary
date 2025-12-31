using GameLibrary.Domain.Domains.Interface;
using GameLibrary.Domain.Utils;
using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Repository.Interface;


namespace GameLibrary.Domain.Domains
{
    public class UserDomain(IUserRepository userRepository) : IUserDomain
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }
        public async Task CreateUserAsync(User user, string password)
        {
            if (string.IsNullOrEmpty(user.Username))
                throw new ArgumentException("Username cannot be empty");

            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException("Email cannot be empty");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be empty");

            if (password.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters");

            if (await _userRepository.UsernameExistsAsync(user.Username))
                throw new ArgumentException("Username already exists");

            if (await _userRepository.EmailExistsAsync(user.Email))
                throw new ArgumentException("Email already exists");

            user.PasswordHash = PasswordHasher.Hash(password);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdateUserProfileAsync(int id, string username, string email)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new ArgumentException("User not found");

            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username cannot be empty");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be empty");

            var existingUser = await _userRepository.GetByUsernameAsync(username);
            if (existingUser != null && existingUser.Id != id)
                throw new ArgumentException("Username already exists");

            existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null && existingUser.Id != id)
                throw new ArgumentException("Email already exists");

            user.Username = username;
            user.Email = email;

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(int id, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new ArgumentException("User not found");

            if (string.IsNullOrEmpty(user.PasswordHash) ||
                !PasswordHasher.Verify(currentPassword, user.PasswordHash))
                throw new ArgumentException("Current password is incorrect");

            if (string.IsNullOrEmpty(newPassword))
                throw new ArgumentException("New password cannot be empty");

            if (newPassword.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters");

            user.PasswordHash = PasswordHasher.Hash(newPassword);

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.SoftDeleteAsync(id);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> ValidatePasswordAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null || string.IsNullOrEmpty(user.PasswordHash))
                return false;

            return PasswordHasher.Verify(password, user.PasswordHash);
        }
    }
}
