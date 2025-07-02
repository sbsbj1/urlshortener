using Microsoft.AspNetCore.Http.HttpResults;
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

        private readonly IUrlService _urlService;
       

        public UrlShortenerController(IUrlService urlService )
        {

            _urlService = urlService;
        }



        //:POST
        [HttpPost]
        public async Task<IActionResult> PostShortenUrl([FromBody] UrlDto url)
        {

            if (url.Url == null)
            {
                return BadRequest("La Url Original: LongUrl es obligatoria.");
            }



            var model = await _urlService.ShortenUrl(url.Url);

            return CreatedAtAction(nameof(GetUrlItem), new { Key = model.Key }, model);
        }

        //:GET
        [HttpGet]
        public async Task<IActionResult> GetUrlItem(string key)
        {
            var urlItem =  await _urlService.GetUrl(key);
            if (urlItem == null)
            {
                return NotFound();
            }
            return Ok(urlItem);
        }

        //:GET REDIRECT
        [HttpGet("{key}")]
        public async Task<IActionResult> Redirect(string key)
        {
            
            var dbModel = await _urlService.GetUrl(key);
            if (dbModel != null)
            {
                
                return base.Redirect(dbModel.LongUrl);

            }
            return NotFound();
        }

        //:DELETE
        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete(string key)
        {

            var dbShortUrl = await _urlService.GetUrl(key);
            if(dbShortUrl != null)
            {
                await _urlService.DeleteUrl(key);
                return Ok("Url deleted");
            }
            return NotFound("Url doesn't exist");

        }

    }
}
