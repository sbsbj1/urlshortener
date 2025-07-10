namespace UrlShortener.Interfaces
{
    public interface IAuthService
    {
        public string CreateToken();

        public string HashPassword(string password);

        public bool ComparePassword(string password, string hashString);
    }
}
