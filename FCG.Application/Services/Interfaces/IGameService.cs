using FCG.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Services.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameDTO>> GetAllGamesAsync();
        Task<GameDTO> GetGameByIdAsync(int id);
        Task<GameDTO> CreateGameAsync(CreateGameModel model);
        Task UpdateGameAsync(int id, UpdateGameModel model);
        Task DeleteGameAsync(int id);
    }
}
