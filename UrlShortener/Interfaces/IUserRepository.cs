using UrlShortener.Model;

namespace UrlShortener.Interfaces
{
    public interface IUserRepository
    {
        //USER
        public Task Add(User user);
        public Task<User> GetUser(string email);

        public Task Update(User user);

        public Task<User> GetToken(string token);
    }
}
