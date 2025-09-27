using OnlineBoutiqueDataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBoutiqueCoreLayer.Services
{
    public interface IUserService
    {
        public Task<bool> RegisterAsync(string name, string email, string password);
        public Task<User> AuthenticateAsync(string email, string password);
        public Task<List<User>> GetAllUsersAsync();
        public Task<bool> DeleteUserAsync(int userId);
    }
}
