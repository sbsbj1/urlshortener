using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Model
{
    public class UrlModel
    {
        [Key]
        public string Key { get; set; }   //encodedUrl
        public string LongUrl { get; set; }  //url
        public string ShortUrl { get; set; } //protocol+encodedUrl

        public string IdempotencyKey { get; set; }
        public string UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
