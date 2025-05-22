using FCG.Application.DTO;
using FCG.Application.Services.Interfaces;
using FCG.Domain.Entities;
using FCG.Domain.Exceptions;
using FCG.Domain.Interfaces;
using FCG.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<IEnumerable<GameDTO>> GetAllGamesAsync()
        {
            var games = await _gameRepository.GetAllAsync();

            var gamesDto = games.Select(game => new GameDTO
            {
                Id = (int)game.Id,
                Title = game.Title,
                Price = game.Price,
                Description = game.Description,
                Genre = game.Genre
            }).ToList();

            return gamesDto;
        }

        public async Task<GameDTO> GetGameByIdAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);

            if (game == null)
                throw new BusinessErrorDetailsException("Jogo não encontrado.");

            return new GameDTO
            {
                Id = (int)game.Id,
                Title = game.Title,
                Price = game.Price,
                Description = game.Description,
                Genre = game.Genre
            };
        }

        public async Task<GameDTO> CreateGameAsync(CreateGameModel model)
        {
            var game = new Game
            (
                model.Title,
                (Price)model.Price,
                model.Description,
                model.Genre
            );

            await _gameRepository.AddAsync(game);

            return new GameDTO
            {
                Id = (int)game.Id,
                Title = game.Title,
                Price = game.Price,
                Description = game.Description,
                Genre = game.Genre
            };
        }

        public async Task UpdateGameAsync(int id, UpdateGameModel model)
        {
            var game = await _gameRepository.GetByIdAsync(id);

            var updatedGame = new Game(
                id,
                model.Title,
                (Price)model.Price,
                model.Description,
                model.Genre
            );

            await _gameRepository.UpdateAsync(updatedGame);
        }

        public async Task DeleteGameAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);

            if (game == null)
                throw new BusinessErrorDetailsException("Jogo não encontrado.");
            
            await _gameRepository.DeleteAsync(id);

        }
    }
}
