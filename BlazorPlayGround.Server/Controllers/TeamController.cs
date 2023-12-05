using BlazorPlayGround.Server.Services.CharacterService;
using BlazorPlayGround.Server.Services.TeamService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorPlayGround.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Team>>> GetAllTeam()
        {

            return await _teamService.GetAllTeam();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetSingleTeam(int id)
        {
            var result = await _teamService.GetSingleTeam(id);
            if (result is null)
                return NotFound("Team Not Found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Team>>> AddTeam(Team hero)
        {
            var result = await _teamService.AddTeam(hero);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Team>> UpdateTeam(int id, Team request)
        {
            var result = await _teamService.UpdateTeam(id, request);
            if (result is null)
                return NotFound("Team Not Found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Team>> DeleteTeam(int id)
        {
            var result = await _teamService.DeleteTeam(id);
            if (result is null)
                return NotFound("Team Not Found");

            return Ok(result);
        }
    }
}
