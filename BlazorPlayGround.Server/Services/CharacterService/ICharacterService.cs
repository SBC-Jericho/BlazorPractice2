using BlazorPlayGround.Shared.DTOs;

namespace BlazorPlayGround.Server.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<List<Character>> GetAllCharacter();
        Task<Character?> GetSingleCharacter(int id);
        Task<List<Character>> AddCharacter(CharacterDTO hero);
        Task<List<Character>?> UpdateCharacter(int id, CharacterDTO request);
        Task<List<Character>?> DeleteCharacter(int id);


    }
}
