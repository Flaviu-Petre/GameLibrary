using GameLibrary.Entity.Entities;
using GameLibrary.Repository.Context;
using GameLibrary.Repository.Repositories;
using GameLibrary.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary.Repository.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(GameLibraryDbContext context) : base(context)
        {
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await GetQueryable()
                .AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await GetQueryable()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await GetQueryable()
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await GetQueryable()
                .AnyAsync(u => u.Username == username);
        }
    }
}
