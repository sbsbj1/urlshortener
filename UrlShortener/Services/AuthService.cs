using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using UrlShortener.DTOs;
using UrlShortener.Interfaces;
using UrlShortener.Model;

namespace UrlShortener.Services
{
    public class AuthService : IAuthService
    {

        private const int _saltSize = 16;
        private const int _keySize = 32;
        private const int _iterations = 100000;
        private static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256;
        private const char segmentDelimiter = ':';



        private readonly IUserRepository _userRepository;
        public IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public bool ComparePassword(string password, string hashString)
        {
            string[] segments = hashString.Split(segmentDelimiter);
            byte[] hash = Convert.FromHexString(segments[0]);
            byte[] salt = Convert.FromHexString(segments[1]);
            int iterations = int.Parse(segments[2]);
            HashAlgorithmName algorithmName = new HashAlgorithmName(segments[3]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                algorithmName,
                hash.Length
                );
            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
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

        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                _iterations,
                _algorithm,
                _keySize
                );

            return string.Join(segmentDelimiter, Convert.ToHexString(hash), Convert.ToHexString(salt), _iterations, _algorithm);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _userRepository.GetUser(email);
        }

        public async Task<User> CreateUser(UserRegisterDTO userDto)
        {
            var rand = new Random();
            var password = HashPassword(userDto.Password);
            var token = Math.Floor(100000 + rand.NextDouble() * 900000).ToString();


            var newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = userDto.Name,
                Email = userDto.Email,
                Password = password,
                Token = token,
                Confirmed = false,
                UrlModels = new List<UrlModel>()
            };

            await _userRepository.Add(newUser);
            return newUser;
        }
    }
}
