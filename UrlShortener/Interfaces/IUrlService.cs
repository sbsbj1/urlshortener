using UrlShortener.Model;

namespace UrlShortener.Interfaces
{
    public interface IUrlService
    {
        public Task<UrlModel> ShortenUrl(string url);
        public Task<UrlModel?> GetUrl(string key);
        public Task<bool> DeleteUrl(string key, string userId);

        public Task<List<UrlModel?>> GetAllUrlById(string userId);
    }
}
