
namespace BlazorPlayGround.Server.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
                new Character
                {
                Id = 1,
                  Name = "Spider Man",
                  Bio = "Amazing Spiderman",
                  BirthDate = DateTime.Now,
                  Image = "https://seeklogo.com/images/S/spider-man-comic-new-logo-322E9DE914-seeklogo.com.png",
                  TeamId = 1,
                  DifficultyId = 1,
                  isReadyToFight = true,
                },
                new Character
                {
                Id = 2,
                  Name = "Batman",
                  Bio = "Night Owl",
                  BirthDate = DateTime.Now,
                  Image = "https://icon-library.com/images/batman-icon-png/batman-icon-png-14.jpg",
                  TeamId = 2,
                  DifficultyId = 2,
                  isReadyToFight = true,
                }

        };
        public List<Character> AddCharacter(Character hero)
        {
            characters.Add(hero);
            return characters;
        }

        public List<Character>? DeleteCharacter(int id)
        {
            var hero = characters.Find(x => x.Id == id);
            if (hero is null)
                return null;

            characters.Remove(hero);
            return characters;
        }

        public List<Character> GetAllCharacter()
        {
            return characters;
        }

        public Character? GetSingleCharacter(int id)
        {

            var hero = characters.Find(x => x.Id == id);
            if (hero is null)
                return null;
            return hero;
        }

        public List<Character>? UpdateCharacter(int id, Character request)
        {
            var hero = characters.Find(x => x.Id == id);
            if (hero is null)
                return null;

            hero.Name = request.Name;
            hero.Bio = request.Bio;
            hero.BirthDate = request.BirthDate;
            hero.Image = request.Image;
            hero.TeamId = request.TeamId;
            hero.DifficultyId = request.DifficultyId;
            hero.isReadyToFight = request.isReadyToFight;

            return characters;
        }
    }
}
