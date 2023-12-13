using BlazorPlayGround.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlazorPlayGround.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> Register(UserDTO request);
        Task<string> Login(UserLoginDTO request);
        Task<List<User>> GetAllUser();


    }
}
