using UrlShortener.Model;

namespace UrlShortener.Interfaces
{
    public interface IUrl
    {

        public Task<UrlModel> ShortenUrl(string url);


        public Task<UrlModel> GetByKey(string key);

    }
}
