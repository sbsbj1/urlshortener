using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Model
{

    public class User
    {
        [Key]
        public string id { get; set; }
        [Required]
        public string name { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public string token { get; set; }
        public bool confirmed { get; set; }
        public ICollection<UrlModel> UrlModels { get; set; }

        

    }
}
