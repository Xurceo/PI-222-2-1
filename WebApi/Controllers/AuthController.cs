using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Infrastructure;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TokenProvider _tokenProvider;
        public AuthController(IUserService userService, TokenProvider tokenProvider)
        {
            _userService = userService;
            _tokenProvider = tokenProvider;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginDTO dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest("Username and password are required.");
            }
            try
            {
                var user = await _userService.Authenticate(dto);
                if (user == null)
                {
                    return Unauthorized("Invalid username or password.");
                }
                var token = _tokenProvider.GenerateToken(user);

                Response.Cookies.Append("jwt", token, new CookieOptions
                {
                    HttpOnly = true, // cookie не доступна JS
                    Secure = true,   // true в ПРОДІ!!!!
                    SameSite = SameSiteMode.Strict, // или Lax, если нужно
                    Path = "/", // доступна на всьому сайті
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });

                return Ok(new { User = user, Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("logout")]
        public ActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok("Logged out successfully.");
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }
            var userGuid = Guid.Parse(userId);
            var user =  await _userService.GetById(userGuid);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(new { User = user });
        }
    }
}
