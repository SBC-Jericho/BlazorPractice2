namespace BlazorPlayGround.Server.Services.CharacterService
{
    public interface ICharacterService
    {
        List<Character> GetAllCharacter();
        Character? GetSingleCharacter(int id);
        List<Character> AddCharacter(Character hero);
        List<Character>? UpdateCharacter(int id, Character request);
        List<Character>? DeleteCharacter(int id);


    }
}
