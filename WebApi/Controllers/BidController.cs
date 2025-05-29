using BLL.DTOs;
using BLL.Interfaces;
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

        [HttpPost]
        public async Task<ActionResult<Guid>> PlaceBid([FromBody] BidDTO dto)
        {
            var id = await _biddingService.PlaceBid(dto);
            return CreatedAtAction(nameof(PlaceBid), new { id }, dto);
        }
    }
}

