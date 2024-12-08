using ApiService.Interfaces;
using ApiService.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiService.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _jwtSecret;

        public JwtService(IConfiguration configuration)
        {
            _jwtSecret = configuration["Jwt:Key"];
        }

        public string GenerateJwtToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]{
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.Role),
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "localhostDev", // ตรงกับ ValidIssuer
                Audience = "localhostDev", // ตรงกับ ValidAudience
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
