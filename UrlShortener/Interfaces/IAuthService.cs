using UrlShortener.DTOs;
using UrlShortener.Model;

namespace UrlShortener.Interfaces
{
    public interface IAuthService
    {
        public string CreateToken();

        public string HashPassword(string password);

        public bool ComparePassword(string password, string hashString);

        Task<User> CreateUser(UserRegisterDTO userDto);

        public Task<User?> GetUserByEmail(string email);

    }
}
