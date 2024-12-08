using ApiService.Data;
using ApiService.Interfaces;
using ApiService.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<UserModel> Authenticate(string username, string password)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            return user;
        }

        public async Task<bool> Register(UserModel user)
        {
            if (await _dataContext.Users.AnyAsync(u => u.Username == user.Username))
            {
                return false;
            }
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
