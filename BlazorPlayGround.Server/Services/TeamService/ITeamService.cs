namespace BlazorPlayGround.Server.Services.TeamService
{
    public interface ITeamService
    {
        Task<List<Team>> GetAllTeam();
        Task<Team?> GetSingleTeam(int id);
        Task<List<Team>> AddTeam(Team team);
        Task<List<Team>?> UpdateTeam(int id, Team request);
        Task<List<Team>?> DeleteTeam(int id);
    }
}
