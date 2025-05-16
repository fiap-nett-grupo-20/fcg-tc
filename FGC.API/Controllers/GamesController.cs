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

        private readonly IGameRepository _gameRepository;

        public GamesController(IGameRepository gameRepository, IGameService gameService)
        {
            _gameRepository = gameRepository;
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

       
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateGameModel model)
        {
            try
            {
                var game = await _gameRepository.GetByIdAsync(model.Id);

                game.Title = model.Title;
                game.Description = model.Description;
                game.Genre = model.Genre;

                await _gameRepository.UpdateAsync(game);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGame([FromRoute] int id)
        {
            
            await _gameService.DeleteGameAsync(id);

            return Ok();
           
        }
    }
}
