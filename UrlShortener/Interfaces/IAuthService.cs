using Microsoft.Extensions.Primitives;
using Microsoft.Identity.Client;
using UrlShortener.DTOs;
using UrlShortener.Model;

namespace UrlShortener.Interfaces
{
    public interface IAuthService
    {
        public string CreateToken(User user);

        public string HashPassword(string password);

        public bool ComparePassword(string password, string hashString);

        Task<User> CreateUser(UserRegisterDTO userDto);

        Task<(string? token, string? errorMessage)> Login(UserLoginDTO userDto);

        public Task<User?> GetUserByEmail(string email);

        public Task<bool> ConfirmAccount(string token);

    }
}
