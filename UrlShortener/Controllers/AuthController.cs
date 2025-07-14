
using Microsoft.AspNetCore.Mvc;
using UrlShortener.DTOs;
using UrlShortener.Interfaces;

namespace UrlShortener.Controllers
{
    [Route("/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }



        //CREATE USER
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] UserRegisterDTO user)
        {
            if(user.Email == null || user.Password == null)
            {
                return BadRequest("You must complete the requested fields");
            }

            var existingUser = await _authService.GetUserByEmail(user.Email);
            if(existingUser != null)
            {
                return Conflict(new { error = "User already exists" });
            }
            try
            {
                var newUser = await _authService.CreateUser(user);
                return StatusCode(201, new { message = "User created succefully!" });
            }
            catch(Exception ex)
            {
                Console.WriteLine($"❌ Error en CreateUser: {ex.Message}");
                return StatusCode(500, new { error = $"An error has occurred: {ex.Message}" });
            }
        }
    }
}
