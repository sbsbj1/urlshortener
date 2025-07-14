using Microsoft.EntityFrameworkCore;
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
        public async Task<User> GetUser(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
