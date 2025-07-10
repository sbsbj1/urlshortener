using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using UrlShortener.Interfaces;
using UrlShortener.Model;

namespace UrlShortener.Services
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository _urlRepository;


        public UrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task DeleteUrl(string key)
        {
            var url = await _urlRepository.GetByKey(key);
            if (url != null) 
            {
                await _urlRepository.Delete(key);
            }
        }

        public async Task<UrlModel?> GetUrl(string key)
        {
            return await _urlRepository.GetByKey(key);
        }

        public async Task<UrlModel> ShortenUrl(string url)
        {

            var existing = await _urlRepository.GetByLongUrl(url);
            if (existing != null)
            {
                return existing;
            }



            string protocol = "https://";
            string[] subUrl = url.Split(new string[] { protocol }, StringSplitOptions.None);
            byte[] binaryUrl = System.Text.Encoding.UTF8.GetBytes(subUrl[1]);
            MD5 md5H = MD5.Create();
            byte[] hashedData = md5H.ComputeHash(binaryUrl);


            byte[] shortHashedData = new byte[4];
            System.Buffer.BlockCopy(hashedData, 0, shortHashedData, 0, 4);

            string hashedDataToBase64 = Convert.ToBase64String(shortHashedData)
                 .Replace('/', '-')
                 .TrimEnd('=');

            string shortUrl = $"https://localhost7188/{hashedDataToBase64}";


            var urlModel = new UrlModel
            {
                Key = hashedDataToBase64,
                LongUrl = url,
                ShortUrl = shortUrl,
                IdempotencyKey = hashedDataToBase64
            };


            await _urlRepository.Add(urlModel);

            return urlModel;



            /*

            string encodedUrl = Convert.ToBase64String(binaryUrl);




            Console.WriteLine(url);
            Console.WriteLine(binaryUrl[0]);
            Console.WriteLine(subUrl[1]);
            Console.WriteLine(encodedUrl);
            */

        }


    }
}
