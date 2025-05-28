namespace FCG.Application.DTO
{
    public class LoginDto
    {
        /// <summary>
        /// Email do usuário
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
