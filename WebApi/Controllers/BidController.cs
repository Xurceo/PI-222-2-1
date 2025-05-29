using BLL.CreateDTOs;
using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var bids = await _biddingService.GetAll();
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
        public async Task<ActionResult<Guid>> PlaceBid(Guid lotId, decimal amount)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            Guid userGuid;
            if (!string.IsNullOrEmpty(userId))
                userGuid = Guid.Parse(userId);
            else
            {
                return Unauthorized(userId);
            }

            var id = await _biddingService.PlaceBid(userGuid, lotId, amount);
            return CreatedAtAction(nameof(PlaceBid), new { id });
        }
    }
}

