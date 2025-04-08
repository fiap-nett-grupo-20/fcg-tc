using System.ComponentModel.DataAnnotations;

namespace FGC.API.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string LanguageName { get; set; }
    }
}
