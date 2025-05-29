using Microsoft.AspNetCore.Mvc;
using UrlShortener.Interfaces;
using UrlShortener.Model;
using UrlShortener.Repository;


namespace UrlShortener.Controllers
{
    [Route("/")]
    [ApiController]
    public class UrlShortenerController : Controller
    {

        private readonly IUrl _UrlRepository;
        private readonly UrlContext _DbContext;

        public UrlShortenerController(IUrl urlRepository, UrlContext urlContext)
        {
            _UrlRepository = urlRepository;
            _DbContext = urlContext;
        }

        public class LongUrlRequest
        {
            public string LongUrl{ get; set; }
        }


        //:POST
        [HttpPost]
        public async Task<IActionResult> PostShortenUrl([FromBody] UrlDto url)
        {

            if(url.Url == null)
            {
                return BadRequest("La Url Original: LongUrl es obligatoria.");
            }
           
            var existing = _DbContext.Urls.FirstOrDefault(u => u.LongUrl == url.Url);
            if (existing != null)
            {
                
                return Conflict(new { message = "Request has already been processed.", item =  existing });
            }

           var model = await _UrlRepository.ShortenUrl(url.Url);

            return CreatedAtAction(nameof(GetUrlItem), new { Key = model.Key }, model);
        }

        //:GET
        [HttpGet]
        public async Task<IActionResult> GetUrlItem(string key)
        {
            var urlItem = await _UrlRepository.GetByKey(key);
            if(urlItem == null)
            {
                return NotFound();
            }
            return Ok(urlItem);
        }

        //:GET REDIRECT
        [HttpGet("{key}")]
        public async Task<IActionResult> Redirect(string key)
        {
            Console.WriteLine($"FIRST {key}");
            var fullShortUrl = $"https://localhost7188/{key}";
            Console.WriteLine($" SECOND{fullShortUrl}");
            var dbModel = _DbContext.Urls.SingleOrDefault(db => db.Key == key);
            if(dbModel != null)
            {
                Console.WriteLine($"Se encontro la url : {dbModel.LongUrl}");
                return  base.Redirect(dbModel.LongUrl);
                
            }
            return NotFound();

            
        }

    }
}
