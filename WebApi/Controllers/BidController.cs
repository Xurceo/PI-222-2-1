using BLL.CreateDTOs;
using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/bids")]
    public class BidController : ControllerBase
    {
        private readonly IBiddingService _biddingService;

        public BidController(IBiddingService biddingService)
        {
            _biddingService = biddingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BidDTO>>> GetAll()
        {
            var bids = await _biddingService.GetAllShort();
            return Ok(bids);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BidDTO>> GetById(Guid id)
        {
            var bid = await _biddingService.GetById(id);
            if (bid == null)
            {
                return NotFound();
            }
            return Ok(bid);
        }

        [HttpGet("lot/{lotId}")]
        public async Task<ActionResult<IEnumerable<BidDTO>>> GetBidsByLotId(Guid lotId)
        {
            var bids = await _biddingService.GetBidsByLotId(lotId);
            if (bids == null || !bids.Any())
            {
                return NotFound();
            }
            return Ok(bids);
        }

        [Authorize(Roles = "MANAGER,USER,ADMIN")]
        [HttpPost]
        public async Task<ActionResult<Guid>> PlaceBid([FromBody] PlaceBidDTO dto)
        {
            Guid id;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid userGuid;
            if (!string.IsNullOrEmpty(userId))
                userGuid = Guid.Parse(userId);
            else
            {
                return Unauthorized(userId);
            }

            try
            {

                id = await _biddingService.PlaceBid(userGuid, dto.LotId, dto.Amount);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            return Ok("Bid placed successfully.");
        }
    }
}

