using System.ComponentModel.DataAnnotations;

namespace FGC.API.Models
{
    public class UpdateUserModel
    {
        public string? NameUser { get; set; }

        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
