using ApiService.Models;

namespace ApiService.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> Authenticate(string username, string password);
        Task<bool> Register(UserModel user);
    }
}
