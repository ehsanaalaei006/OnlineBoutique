using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBoutiqueDataLayer.Context;
using OnlineBoutiqueDataLayer.Entities;

namespace OnlineBoutiqueCoreLayer.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _hasher = new();

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterAsync(string name, string email, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Email == email))
                return false;

            var user = new User
            {
                Name = name,
                Email = email,
            };
            user.PasswordHash = _hasher.HashPassword(user, password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
