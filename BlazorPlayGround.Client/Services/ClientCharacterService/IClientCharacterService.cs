using BlazorPlayGround.Shared.Models;

namespace BlazorPlayGround.Client.Services.ClientCharacterService
{
    public interface IClientCharacterService
    {
        List<Character> ClientCharacter { get; set; }
        Task <List<Character>> GetAllCharacter();
        Task<Character?> GetSingleCharacter(int id);
        Task AddCharacter(Character hero);
        Task UpdateCharacter(int id, Character request);
        Task DeleteCharacter(int id);
    }
}
