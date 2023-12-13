using BlazorPlayGround.Server.Services.AuthService;
using BlazorPlayGround.Server.Services.CharacterService;
using BlazorPlayGround.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlazorPlayGround.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _contextAccessor;
      
        public AuthController(IConfiguration configuration, IAuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _authService = authService;
            _contextAccessor = httpContextAccessor; 
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO request) 
        {

            var result = await _authService.Register(request);
            return Ok(result);
        }

        [HttpPost("login")]

        public async Task<ActionResult<string>> Login(UserLoginDTO request) 
        {
            var result = await _authService.Login(request);
            if (result == "No User Found" || result == "Wrong password.")
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUser()
        {

            return await _authService.GetAllUser();
        }

        private string CreateToken(User user) 
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) 
        {
            using (var hmac = new HMACSHA512()) 
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) 
        {
            using (var hmac = new HMACSHA512(passwordSalt)) 
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}
