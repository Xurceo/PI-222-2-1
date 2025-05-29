using BLL.DTOs;
using BLL.CreateDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BLL.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LotDTO>>> GetAll()
        {
            var lots = await _lottingService.GetAll();
            return Ok(lots);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LotDTO>> GetById(Guid id)
        {
            var lot = await _lottingService.GetById(id);
            if (lot == null)
                return NotFound();
            return Ok(lot);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<LotDTO>>> GetAllByCategoryId(Guid categoryId)
        {
            var lots = await _lottingService.GetAllByCategoryId(categoryId);
            return Ok(lots);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddLot([FromBody] CreateLotDTO dto)
        {
            var id = await _lottingService.AddLot(dto);
            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLot([FromBody] LotDTO dto)
        {
            await _lottingService.UpdateLot(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLot(Guid id)
        {
            try
            {
                await _lottingService.DeleteLot(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        [Authorize(Roles="MANAGER")]
        [HttpPut("{lotId}/confirm")]
        public async Task<ActionResult<Guid>> ConfirmLot(Guid lotId)
        {
            try
            {
                await _lottingService.ConfirmLot(lotId);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok($"Lot with id: {lotId} confirmed");
        }
    }
}
