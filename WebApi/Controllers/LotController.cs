using BLL.DTOs;
using BLL.CreateDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BLL.Exceptions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DAL.Enums;
using BLL.ShortDTOs;

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
        public async Task<ActionResult<IEnumerable<LotShortDTO>>> GetAll()
        {
            var lots = await _lottingService.GetAllShort();
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

        [Authorize(Roles = "MANAGER,ADMIN")]
        [HttpPost]
        public async Task<ActionResult<Guid>> AddLot([FromBody] CreateLotDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            dto.OwnerId = Guid.Parse(userId);

            var id = await _lottingService.AddLot(dto);
            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }

        [Authorize(Roles = "MANAGER,ADMIN")]
        [HttpPut]
        public async Task<IActionResult> UpdateLot([FromBody] LotDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)
                || !Guid.TryParse(userId, out Guid userGuid) && userGuid != dto.Owner.Id)
                return Unauthorized("You are not authorized to update this lot.");
            await _lottingService.UpdateLot(dto);
            return NoContent();
        }

        [Authorize(Roles = "MANAGER,ADMIN")]
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

        [Authorize(Roles = "MANAGER,ADMIN")]
        [HttpPut("{lotId}/confirm")]
        public async Task<ActionResult<Guid>> ConfirmLot(Guid lotId)
        {
            try
            {
                await _lottingService.ChangeLotStatus(lotId, LotStatus.Confirmed);
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
