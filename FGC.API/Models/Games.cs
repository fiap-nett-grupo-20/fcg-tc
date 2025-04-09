using System.ComponentModel.DataAnnotations;

namespace FGC.API.Models
{
    public class Games
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } //titulo

        [Required]
        public string Genre { get; set; } //genero

        [Required]
        public string Size { get; set; } //tamanho

        [Required]
        public string Category { get; set; } //categoria

        [Required]
        public DateTime ReleaseDate { get; set; } //ano de lançamento

        [Required]
        [StringLength(100)]
        public string Platform { get; set; } //plataforma onde o jogo roda

        [Required]
        [StringLength(100)]
        public string Developer { get; set; } //desenvolvedor 

        [Required]
        [StringLength(100)]
        public string Publisher { get; set; }  //quem lançou o jogo

        [Required]
        [StringLength(10)]
        public string AgeRating { get; set; } //faixa etária

        [Required]
        [StringLength(50)]
        public string GameMode { get; set; } //Modo de Jogo, se é single-player, multiplayer, cooperativo, etc.

        [Required]
        public decimal Price { get; set; } //preço

        [Required]
        [StringLength(500)]
        public string Description { get; set; } //descrição

        [Required]
        [StringLength(50)]
        public Language Languages { get; set; } //idiomas

        [Range(0, 10)]
        public double Rating { get; set; } //avaliações

        [StringLength(500)]
        public string SystemRequirements { get; set; } //requisitos de sistema 

        [Range(0, 1000)]
        public int AverageDuration { get; set; } //tempo médio para acabar o modo história

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; } //capa do jogo

        public int UserId { get; set; }
        public Users User { get; set; }
        //relacionamento com a tabela de usuarios
    }
}
