using BlazorPlayGround.Shared.Models;

namespace BlazorPlayGround.Client.Services.ClientTeamService
{
    public interface IClientTeamService
    {
        List<Team> ClientTeam { get; set; }
        Task<List<Team>> GetAllTeam();
        Task<Team?> GetSingleTeam(int id);
        Task AddTeam(Team team);
        Task UpdateTeam(int id, Team request);
        Task DeleteTeam(int id);
    }
}
