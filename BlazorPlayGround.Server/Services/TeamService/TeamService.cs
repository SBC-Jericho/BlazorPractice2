
using BlazorPlayGround.Client.Pages;
using BlazorPlayGround.Shared.Models;

namespace BlazorPlayGround.Server.Services.TeamService
{
    public class TeamService : ITeamService
    {
        //Dependency injection
        private readonly DataContext _context;

        //constructor to inject the DataContext again
        public TeamService(DataContext context)
        {
            _context = context;
        }

        private static List<Team> teams = new List<Team>
        {
                new Team
                {
                  Id = 1,
                  Name = "Avengers",
                  
                },
                new Team
                {
                  Id = 2,
                  Name = "Justice League",
                }

        };
        public async Task<List<Team>> AddTeam(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return await _context.Teams.ToListAsync();
        }

        public async Task<List<Team>?> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team is null)
                return null;

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();


            return await _context.Teams.ToListAsync();
        }

        public async Task<List<Team>> GetAllTeam()
        {
            var character = await _context.Teams.ToListAsync();
            return character;
        }

        public async Task<Team?> GetSingleTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team is null)
                return null;
            return team;
        }

        public async Task<List<Team>?> UpdateTeam(int id, Team request)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team is null)
                return null;

            team.Name = request.Name;

            await _context.SaveChangesAsync();

            return await _context.Teams.ToListAsync();
        }
    }
}
