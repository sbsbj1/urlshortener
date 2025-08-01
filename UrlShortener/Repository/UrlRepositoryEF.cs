using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UrlShortener.Interfaces;
using UrlShortener.Model;

namespace UrlShortener.Repository
{
    public class UrlRepositoryEF : IUrlRepository
    {

        private readonly UrlContext _context;
       

        public UrlRepositoryEF(UrlContext context)
        {
            _context = context;
        }

        public async Task Add(UrlModel urlModel)
        {
            _context.Add(urlModel);
            await _context.SaveChangesAsync();
        }



        public async Task Delete(string key)
        {
            var url = await _context.Urls.FindAsync(key);
            if(url != null)
            {
                _context.Urls.Remove(url);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<UrlModel?>> GetAllUrl(string userId)
        {
            var urls = await _context.Urls
                .Where(u => u.UserId == userId)
                .Include(u => u.User).ToListAsync();
            return urls;
        }

        public async Task<UrlModel?> GetByKey(string key)
        {
            return await _context.Urls.SingleOrDefaultAsync(db => db.Key == key);
        }


        public async Task<UrlModel?> GetByLongUrl(string url)
        {
            return await _context.Urls.FirstOrDefaultAsync(u => u.LongUrl == url);
        }

   

    }
}
