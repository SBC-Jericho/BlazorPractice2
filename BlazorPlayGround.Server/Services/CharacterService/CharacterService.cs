
using Azure.Core;
using BlazorPlayGround.Shared.DTOs;

namespace BlazorPlayGround.Server.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        //Dependency injection
        private readonly DataContext _context;

        //constructor to inject the DataContext again
        public CharacterService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Character>> AddCharacter(CharacterDTO hero)
        {
            var newCharacter = new Character
            {
                Name = hero.Name,
                Bio = hero.Bio,
                BirthDate = hero.BirthDate,
                TeamId = hero.TeamId,
                DifficultyId = hero.DifficultyId,
                Image = hero.Image,
                isReadyToFight = hero.isReadyToFight
                
            };

            _context.Add(newCharacter);
            await _context.SaveChangesAsync();
            return await _context.Characters.ToListAsync();
        }

        public async Task<List<Character>?> DeleteCharacter(int id)
        {
            var hero = await _context.Characters.FindAsync(id);
            if (hero is null)
                return null;

            _context.Characters.Remove(hero);
            await _context.SaveChangesAsync();


            return await _context.Characters.ToListAsync();
        }

        public async Task<List<Character>> GetAllCharacter()
        {
            var character = await _context.Characters
                .Include(c => c.Team)
                .Include(c => c.Difficulty)
                .ToListAsync();
            return character;
        }

        public async Task<Character?> GetSingleCharacter(int id)
        {

            var hero = await _context.Characters.FindAsync(id);
            if (hero is null)
                return null;
            return hero;
        }

        public async Task<List<Character>?> UpdateCharacter(int id, CharacterDTO request)
        {
            var hero = await _context.Characters
                .Include (c => c.Team)
                .Include (c => c.Difficulty)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            if (hero is null)
                return null;

            hero.Name = request.Name;
            hero.Bio = request.Bio;
            hero.BirthDate = request.BirthDate;
            hero.Image = request.Image;
            hero.TeamId = request.TeamId;
            hero.DifficultyId = request.DifficultyId;
            hero.isReadyToFight = request.isReadyToFight;

            await _context.SaveChangesAsync();

            return await _context.Characters.ToListAsync();
        }
    }
}
