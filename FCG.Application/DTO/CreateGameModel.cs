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
        public required string Title { get; set; }

        public required decimal Price { get; set; }

        public required string Description { get; set; }

        public required string Genre { get; set; }
    }
}
