using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UrlShortener.Interfaces;
using UrlShortener.Model;

namespace UrlShortener.Repository
{
    public class UserRepositoryEF : IUserRepository
    {

        private readonly UrlContext _context;

        public UserRepositoryEF(UrlContext context)
        {
            _context = context;
        }



        //USER
        public async Task Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetToken(string token)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Token == token);
        }

        public async Task<User> GetUser(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

        }
    }
}
