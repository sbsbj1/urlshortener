using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using UrlShortener.Interfaces;
using UrlShortener.Model;
using UrlShortener.Repository;

namespace UrlShortener.Services
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IUserRepository _userRepository;


        public UrlService(IUrlRepository urlRepository, IUserRepository userRepository)
        {
            _urlRepository = urlRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> DeleteUrl(string key, string userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if(user == null)
            {
                return false;
            }
            var url = await _urlRepository.GetByKey(key);
            if (url == null) 
            {
                return false;
            }
            await _urlRepository.Delete(key);
            return true;
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

        public async Task<List<UrlModel?>> GetAllUrlById(string userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if(user == null)
            {
                return null;
            }
            return await _urlRepository.GetAllUrl(userId);
        }


    }
}
