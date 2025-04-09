namespace FGC.API.Models
{
    using System.ComponentModel.DataAnnotations;

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

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "User"; // Valores possíveis: "Admin", "User", etc.

        // Status do usuário
        [Required]
        public bool IsActive { get; set; } = true;

        // Data de criação e atualização
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Token para recuperação de senha
        [StringLength(500)]
        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiration { get; set; }
    }
}
