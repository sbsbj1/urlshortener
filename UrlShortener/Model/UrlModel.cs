using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener.Model
{
    public class UrlModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Key { get; set; }   //encodedUrl
        public string LongUrl { get; set; }  //url
        public string ShortUrl { get; set; } //protocol+encodedUrl

        public string IdempotencyKey { get; set; }
        public string UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
