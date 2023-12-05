namespace BlazorPlayGround.Server.Services.DifficultyService
{
    public interface IDifficultyService
    {
        Task<List<Difficulty>> GetAllDifficulty();
        Task<Difficulty?> GetSingleDifficulty(int id);
        Task<List<Difficulty>>AddDifficulty(Difficulty Difficulty);
        Task<List<Difficulty>?> UpdateDifficulty(int id, Difficulty request);
        Task<List<Difficulty>?> DeleteDifficulty(int id);

    }
}
