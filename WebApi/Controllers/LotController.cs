using BLL.Interfaces;
using BLL.Models;
using BLL.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/lots")]
    public class LotController : ControllerBase
    {
        private readonly ILottingService _lottingService;

        public LotController(ILottingService lottingService)
        {
            _lottingService = lottingService;
        }

        // Добавьте методы аналогично UserController, используя методы ILottingService
        [HttpGet]
        public ActionResult<IEnumerable<LotDTO>> GetAll()
        {
            var lots = _lottingService.GetAll();
            return Ok(lots);
        }

        [HttpGet("{id:int}")]
        public ActionResult<LotDTO> GetById(int id)
        {
            var user = _lottingService.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public ActionResult AddLot([FromBody] LotDTO dto)
        {
            var id = _lottingService.AddLot(dto);
            return Created($"/api/users/{id}", null);
        }
    }
}
