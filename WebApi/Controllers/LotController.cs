using BLL.Interfaces;
using BLL.DTOs;
using BLL.Service;
using Microsoft.AspNetCore.Mvc;
using BLL.CreateDTOs;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/lots")]
    public class LotController : ControllerBase
    {
        private readonly ILottingService _lottingService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;

        public LotController(
            ILottingService lottingService,
            IUserService userService,
            ICategoryService categoryService
            )
        {
            _lottingService = lottingService;
            _userService = userService;
            _categoryService = categoryService;

        }

        // Добавьте методы аналогично UserController, используя методы ILottingService
        [HttpGet]
        public ActionResult<IEnumerable<LotDTO>> GetAll()
        {
            var lots = _lottingService.GetAll();
            return Ok(lots);
        }

        [HttpGet("{id}")]
        public ActionResult<LotDTO> GetById(Guid id)
        {
            var user = _lottingService.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public ActionResult AddLot([FromBody] CreateLotDTO dto)
        {
            var owner = _userService.GetById(dto.OwnerId);
            if (owner == null)
                return NotFound($"User with id {dto.OwnerId} not found");
            var category = _categoryService.GetById(dto.CategoryId);
            if (category == null)
                return NotFound($"Category with id {dto.CategoryId} not found");

            var id = _lottingService.AddLot(dto);
            return CreatedAtAction(nameof(AddLot), new { id }, dto);
        }
    }
}
