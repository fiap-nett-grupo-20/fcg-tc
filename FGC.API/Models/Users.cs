namespace FGC.API.Models
{
    using System.ComponentModel.DataAnnotations;

    namespace FGC.API.Models
    {
        public class Users
        {
            [Key]
            public int Id { get; set; }

            [Required]
            [StringLength(150)]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [MinLength(8)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "A senha deve ter no mínimo 8 caracteres, incluindo números, letras maiúsculas e minúsculas e caracteres especiais.")]
            public string Password { get; set; }

            public ICollection<Games> Games { get; set; } = new List<Games>();
        }
    }

}
