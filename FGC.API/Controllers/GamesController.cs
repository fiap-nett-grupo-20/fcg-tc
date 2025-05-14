using FCG.Application.DTO;
using FCG.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FCG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;

        public GamesController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var games = await _gameRepository.GetAllAsync();

                var gamesDto = games.Select(game => new GameDTO
                {
                    Id = (int)game.Id, 
                    Title = game.Title ?? "",
                    Price = game.Price,
                    Description = game.Description ?? "",
                    Genre = game.Genre ?? ""
                }).ToList();

                return Ok(gamesDto);
            }
            catch (FormatException  ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GameDTO>> GetGame(int id)
        {
            try
            {
                var game = await _gameRepository.GetByIdAsync(id);
                return Ok(game);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
        }

       /* [HttpPost]
        public async Task<ActionResult<GameDTO>> CreateGame(CreateGameModel model)
        {
            var user = await _userService.CreateUserAsync(model);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }*/

        /*
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
        /*
        [HttpPut]
        public IActionResult Put()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _gameRepository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
    }
}
