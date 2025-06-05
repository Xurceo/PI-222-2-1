using BLL.DTOs;
using BLL.CreateDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BLL.ShortDTOs;

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

        [HttpGet("{id}/bids")]
        public async Task<ActionResult<IEnumerable<BidShortDTO>>> GetUserBids(Guid id)
        {
            var bids = await _userService.GetUserBids(id);
            return Ok(bids);
        }

        [HttpGet("{id}/lots")]
        public async Task<ActionResult<IEnumerable<LotShortDTO>>> GetUserLots(Guid id)
        {
            var lots = await _userService.GetUserLots(id);
            return Ok(lots);
        }

        [Authorize(Roles = "MANAGER,ADMIN")]
        [HttpPost]
        public async Task<ActionResult<Guid>> AddUser([FromBody] CreateUserDTO dto)
        {
            var id = await _userService.AddUser(dto);
            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }

        [Authorize(Roles = "MANAGER,ADMIN")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO dto)
        {
            await _userService.UpdateUser(dto);
            return NoContent();
        }

        [Authorize(Roles = "MANAGER,ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }

        [HttpPut("{id}/password")]
        public async Task<IActionResult> UpdateUserPassword(Guid id, [FromBody] string newPassword)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userGuid = string.IsNullOrEmpty(userId) ? Guid.Empty : Guid.Parse(userId);
            if (userGuid != id && !User.IsInRole("ADMIN") && !User.IsInRole("MANAGER"))
            {
                return Forbid();
            }
            await _userService.UpdateUserPassword(id, newPassword);
            return NoContent();
        }
    }
}
