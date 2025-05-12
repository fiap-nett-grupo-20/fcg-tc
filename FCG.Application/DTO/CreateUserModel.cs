using System.ComponentModel.DataAnnotations;

namespace FCG.Application.DTO
{
    public class CreateUserModel
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        public required string NameUser { get; set; }

        [Required(ErrorMessage = "O nome de login (UserName) é obrigatório.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
        public required string Password { get; set; }

        public List<string>? Roles { get; set; }
    }
}
