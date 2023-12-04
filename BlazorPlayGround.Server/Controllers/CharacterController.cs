
using BlazorPlayGround.Server.Services.CharacterService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorPlayGround.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetAllCharacter() 
        {
            
            return _characterService.GetAllCharacter();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetSingleCharacter(int id)
        {
            var result = _characterService.GetSingleCharacter(id);
            if (result is null)
                return NotFound("Hero Not Found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> AddCharacter(Character hero)
        {
            var result = _characterService.AddCharacter(hero);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Character>> UpdateCharacter(int id, Character request)
        {
            var result = _characterService.UpdateCharacter(id, request);
            if (result is null)
                return NotFound("Hero Not Found");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Character>>DeleteCharacter(int id)
        {
            var result = _characterService.DeleteCharacter(id);
            if (result is null)
                return NotFound("Hero Not Found");

            return Ok(result);
        }
    }
}
