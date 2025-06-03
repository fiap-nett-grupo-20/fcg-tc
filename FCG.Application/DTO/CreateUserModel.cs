namespace FCG.Application.DTO
{
    public class CreateUserModel
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public List<string>? Roles { get; set; }
    }
}
