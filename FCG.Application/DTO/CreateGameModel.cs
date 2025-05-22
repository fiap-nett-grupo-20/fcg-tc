using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.DTO
{
    public class CreateGameModel
    {
        [Required(ErrorMessage = "O nome do jogo é obrigatório.")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Insira um preço para o jogo")]
        public required decimal Price { get; set; }

        public required string Description { get; set; }

        [Required(ErrorMessage = "Insira um genero")]
        public required string Genre { get; set; }
    }
}
