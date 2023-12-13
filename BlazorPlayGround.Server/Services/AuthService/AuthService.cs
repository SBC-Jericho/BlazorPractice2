using BlazorPlayGround.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlazorPlayGround.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        //constructor to inject the DataContext again
        public static User user = new User();
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<List<User>> GetAllUser()
        {
            var users = await _context.user
              .Include(c => c.UserDetails)
              .ToListAsync();
             return users;
        }   

        public async Task<string> Login(UserLoginDTO request)
        {
            var user = await _context.user.FirstOrDefaultAsync(e => e.Username == request.Username);

            if (user == null)
            {
                return "No User Found";
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return "Wrong password.";
            }

            var token = CreateToken(user);

            return token;
        }
       
        public async Task<User>Register(UserDTO request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User new_user = new User
            {
                Username = request.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = request.Role
            };

            UserDetails new_details = new UserDetails
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Contact = request.Contact,
                Address = request.Address,
                User = new_user,
                UserId = new_user.Id
            };

            _context.user.Add(new_user);
            _context.userDetails.Add(new_details);
            await _context.SaveChangesAsync();

            return new_user;

        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
    {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role)
    };

            // 403 Don't have the Correct Role 
            // 401 No Autherization Header Set

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken
                (
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
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
