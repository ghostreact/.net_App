using ApiService.Data;
using ApiService.Interfaces;
using ApiService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            var user = new UserModel
            {
                Username = model.Username,
                Password = BCrypt.Net.BCrypt.HashPassword( model.Password),
                Role = model.Role
            };

            var result = await _userService.Register(user);
            if (!result)
            {
                return BadRequest("User already exists");
            }

            return Ok("Register Successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);
            if (user == null)
            {
                Console.WriteLine(user);
                return Unauthorized("Invalid");
            }
            
            var token = _jwtService.GenerateJwtToken(user);
            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile(){
            var username = User.Identity?.Name;
            return Ok(new { message = $"WelCome , {username}" });
        }
    }
}
