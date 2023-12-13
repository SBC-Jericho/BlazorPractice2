using BlazorPlayGround.Shared.DTOs;
using BlazorPlayGround.Shared.Models;

namespace BlazorPlayGround.Client.Services.ClientAuthService
{
    public interface IClientAuthService
    {
        List<User> ClientUser { get; set; }
        Token token { get; set; }
        Task Register(UserDTO request);
        Task <string> Login(UserLoginDTO request);
        Task<List<User>> GetAllUser();

    }
}
