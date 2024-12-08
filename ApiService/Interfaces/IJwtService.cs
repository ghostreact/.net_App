using ApiService.Models;

namespace ApiService.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(UserModel user);
    }
}
