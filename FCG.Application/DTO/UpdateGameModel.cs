using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.DTO
{
    public class UpdateGameModel
    {
        public string? Title { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string? Genre { get; set; }
    }
}
