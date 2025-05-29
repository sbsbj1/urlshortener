using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UrlShortener.Interfaces;
using UrlShortener.Model;

namespace UrlShortener.Repository
{
    public class UrlRepository : IUrl
    {

        private readonly UrlContext _context;

        public UrlRepository(UrlContext context)
        {
            _context = context;
        }

        

        async Task<UrlModel> IUrl.ShortenUrl(string url)
        {
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

            var existing = await _context.Urls.FindAsync(hashedDataToBase64);
            if(existing is not null)
            {
                return existing;
            }

            var urlModel = new UrlModel
            {
                Key = hashedDataToBase64,
                LongUrl = url,
                ShortUrl = shortUrl,
                IdempotencyKey = hashedDataToBase64
            };
            

            _context.Urls.Add(urlModel);
            await _context.SaveChangesAsync();

            return urlModel;



                /*
                
                string encodedUrl = Convert.ToBase64String(binaryUrl);

                 


                Console.WriteLine(url);
                Console.WriteLine(binaryUrl[0]);
                Console.WriteLine(subUrl[1]);
                Console.WriteLine(encodedUrl);
                */
            
        }

        public Task<UrlModel> GetByKey(string key)
        {
            throw new NotImplementedException();
        }
    }
}
