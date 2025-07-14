using System.ComponentModel.DataAnnotations;

namespace UrlShortener.DTOs
{
    public class UserRegisterDTO
    {

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
