using FCG.Application.DTO;
using FCG.Application.Services.Interfaces;
using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GamesController : ApiBaseController
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGames()
        {
            var games = await _gameService.GetAllGamesAsync();

            return Success(games);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGame(int id)
        {
           var game = await _gameService.GetGameByIdAsync(id);

           return Success(game);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateGame(CreateGameModel model)
        {
            var user = await _gameService.CreateGameAsync(model);

            return Success(user);
            
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateGame(int id, UpdateGameModel model)
        {
            await _gameService.UpdateGameAsync(id,  model);
            return Success("Jogo atualizado com sucesso!");
        }
        
        
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGame([FromRoute] int id)
        {
            
            await _gameService.DeleteGameAsync(id);

            return NoContent();
           
        }
    }
}
