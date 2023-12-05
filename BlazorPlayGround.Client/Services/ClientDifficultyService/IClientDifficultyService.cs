using BlazorPlayGround.Shared.Models;

namespace BlazorPlayGround.Client.Services.ClientDifficultyService
{
    public interface IClientDifficultyService
    {
        List<Difficulty> ClientDifficulty { get; set; }
        Task<List<Difficulty>> GetAllDifficulty();
        Task<Difficulty?> GetSingleDifficulty(int id);
        Task AddDifficulty(Difficulty Difficulty);
        Task UpdateDifficulty(int id, Difficulty request);
        Task DeleteDifficulty(int id);
    }
}
