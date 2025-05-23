using BLL.Interfaces;
using BLL.Models;
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
        public ActionResult<IEnumerable<UserDTO>> GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public ActionResult<UserDTO> GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public ActionResult Add([FromBody] CreateUserDTO dto)
        {
            var id = _userService.AddUser(dto);
            return Created($"/api/users/{id}", null);
        }
    }
}

