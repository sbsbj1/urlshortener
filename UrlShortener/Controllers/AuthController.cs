
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
                return StatusCode(500, new { error ="An error has occurred" });
            }
        }


        //CONFIRM ACCOUNT
        [HttpPost("confirm-account")]
        public async Task<IActionResult> ConfirmAccount([FromBody] string token)
        {
            var isConfirmed = await _authService.ConfirmAccount(token);
            if (!isConfirmed)
            {
                return StatusCode(401, new { error = "Token is not valid" });
            }
            return Ok(new { message = "Account confirmed" });
        }

        //LOGIN 
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLogin)
        {
            var user = await _authService.Login(userLogin);
            if (user.errorMessage != null)
            {
                return StatusCode(500, new { error = $"{user.errorMessage}" });
            }
            return Ok(new { message = $"Login Successful, {user.token}" });
            

        }
    }
}
