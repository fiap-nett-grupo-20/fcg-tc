namespace FCG.Application.DTO
{
    public class UserDto
    {
        public required string Id { get; set; }
        public required string NameUser { get; set; }
        //public required string UserName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string>? Roles { get; set; } // Lista de roles do usuário (opcional)
    }
}
