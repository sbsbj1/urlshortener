using UrlShortener.Model;

namespace UrlShortener.Interfaces
{
    public interface IUrlService
    {
        public Task<UrlModel> ShortenUrl(string url);
        public Task<UrlModel?> GetUrl(string key);
        public Task DeleteUrl(string key);
    }
}
