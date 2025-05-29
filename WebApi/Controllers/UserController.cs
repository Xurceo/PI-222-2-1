using BLL.DTOs;
using BLL.CreateDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetById(Guid id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddUser([FromBody] CreateUserDTO dto)
        {
            var id = await _userService.AddUser(dto);
            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO dto)
        {
            await _userService.UpdateUser(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }

        [HttpPut("{id}/password")]
        public async Task<IActionResult> UpdateUserPassword(Guid id, [FromBody] string newPassword)
        {
            await _userService.UpdateUserPassword(id, newPassword);
            return NoContent();
        }
    }
}
