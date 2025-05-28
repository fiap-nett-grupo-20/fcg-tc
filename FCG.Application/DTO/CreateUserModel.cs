using System.ComponentModel.DataAnnotations;

using FCG.Domain.ValueObjects;

namespace FCG.Application.DTO
{
    public class CreateUserModel
    {
        public required string NameUser { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public List<string>? Roles { get; set; }
    }
}
