using Microsoft.AspNetCore.Mvc;
using OnlineBankingApp.DataLayer.Concrete;
using OnlineBankingApp.Entities.Concrete;
using OnlineBankingApp.Entities.Dtos;

namespace OnlineBankingApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BankingContext _context;
        IConfiguration _configuration;
        public AuthController(BankingContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto user)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                return BadRequest("Username Already Exist");
            }
            var newUser = new User
            {
                Username = user.Username,
                Password = user.Password,
                PasswordHash = PasswordHasher.HashPassword(user.Password)
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok("User Created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto user)
        {
            var loginUser = _context.Users.SingleOrDefault(u => u.Username == user.Username);
            if (loginUser == null || !(PasswordHasher.VerifyPassword(user.Password, loginUser.PasswordHash)))
            {
                return Unauthorized("Invalid username or password");
            }
            //var token = GenerateJwtToken(user);
            var token = OnlineBankingApp.WebAPI.Security.TokenHandler.CreateToken(_configuration);
            return Ok(new { Token = token });
        }
    }
}
