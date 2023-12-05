using BlazorPlayGround.Server.Services.DifficultyService;
using BlazorPlayGround.Server.Services.TeamService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorPlayGround.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyService _difficultyService;
        public DifficultyController(IDifficultyService difficultyService)
        {
            _difficultyService = difficultyService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Difficulty>>> GetAllDifficulty()
        {

            return await _difficultyService.GetAllDifficulty();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Difficulty>> GetSingleDifficulty(int id)
        {
            var result = await _difficultyService.GetSingleDifficulty(id);
            if (result is null)
                return NotFound("Hero Not Found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Difficulty>>> AddDifficulty(Difficulty hero)
        {
            var result = await _difficultyService.AddDifficulty(hero);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Difficulty>> UpdateDifficulty(int id, Difficulty request)
        {
            var result = await _difficultyService.UpdateDifficulty(id, request);
            if (result is null)
                return NotFound("Hero Not Found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Difficulty>> DeleteDifficulty(int id)
        {
            var result = await _difficultyService.DeleteDifficulty(id);
            if (result is null)
                return NotFound("Hero Not Found");

            return Ok(result);
        }
    }
}
