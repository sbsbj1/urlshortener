using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using UrlShortener.Interfaces;

namespace UrlShortener.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUrlRepository _urlRepository;
        public IConfiguration _configuration;

        public AuthService(IUrlRepository urlRepository, IConfiguration configuration)
        {
            _urlRepository = urlRepository;
            _configuration = configuration;
        }

        public bool ComparePassword()
        {
            throw new NotImplementedException();
        }

        public string CreateToken()
        {
            var secretKey = _configuration["SecretKey"];

            byte[] bytes = Encoding.ASCII.GetBytes(secretKey);
            SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(bytes), SecurityAlgorithms.HmacSha256Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = DateTime.UtcNow.AddDays(31),
                    SigningCredentials = signingCredentials
                };
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }

        public string HashPassword()
        {
            throw new NotImplementedException();
        }
    }
}
