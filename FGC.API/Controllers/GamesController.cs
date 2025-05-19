using FCG.Application.DTO;
using FCG.Application.Services.Interfaces;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FCG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetGames()
        {
            var games = await _gameService.GetAllGamesAsync();

            return Ok(games);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GameDTO>> GetGame(int id)
        {
           var game = await _gameService.GetGameByIdAsync(id);

           return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameDTO>> CreateGame(CreateGameModel model)
        {
            var user = await _gameService.CreateGameAsync(model);

            return Ok(user);
            
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGame(int id, UpdateGameModel model)
        {
            await _gameService.UpdateGameAsync(id,  model);
            return Ok();
        }
        
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGame([FromRoute] int id)
        {
            
            await _gameService.DeleteGameAsync(id);

            return Ok();
           
        }
    }
}
