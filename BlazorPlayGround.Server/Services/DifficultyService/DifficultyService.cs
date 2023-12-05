
using BlazorPlayGround.Client.Pages;
using BlazorPlayGround.Shared.Models;

namespace BlazorPlayGround.Server.Services.DifficultyService
{
    public class DifficultyService : IDifficultyService
    {
        //Dependency injection
        private readonly DataContext _context;

        //constructor to inject the DataContext again
        public DifficultyService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Difficulty>> AddDifficulty(Difficulty difficulty)
        {
            _context.Difficulties.Add(difficulty);
            await _context.SaveChangesAsync();
            return await _context.Difficulties.ToListAsync();
        }

        public async Task<List<Difficulty>?> DeleteDifficulty(int id)
        {
            var difficulty = await _context.Difficulties.FindAsync(id);
            if (difficulty is null)
                return null;

            _context.Difficulties.Remove(difficulty);
            await _context.SaveChangesAsync();


            return await _context.Difficulties.ToListAsync();
        }

        public async Task<List<Difficulty>> GetAllDifficulty()
        {
            var character = await _context.Difficulties.ToListAsync();
            return character;
        }

        public async Task<Difficulty?> GetSingleDifficulty(int id)
        {
            var difficulty = await _context.Difficulties.FindAsync(id);
            if (difficulty is null)
                return null;
            return difficulty;
        }

        public async Task <List<Difficulty>?> UpdateDifficulty(int id, Difficulty request)
        {
            var difficulty = await _context.Difficulties.FindAsync(id);
            if (difficulty is null)
                return null;

            difficulty.Title = request.Title;

            await _context.SaveChangesAsync();

            return await _context.Difficulties.ToListAsync();
        }
    }
}
