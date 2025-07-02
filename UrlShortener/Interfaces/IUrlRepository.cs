using UrlShortener.Model;

namespace UrlShortener.Interfaces
{
    public interface IUrlRepository
    {
        public Task<UrlModel?> GetByKey(string key);

        public Task<UrlModel?> GetByLongUrl(string url);

        public Task Add(UrlModel urlModel);

        public Task Delete(string key);

    }
}
