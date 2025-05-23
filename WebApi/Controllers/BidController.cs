using BLL.Interfaces;
using BLL.Models;
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

        // Добавьте методы аналогично UserController, используя методы IBiddingService
        [HttpGet]
        public ActionResult<IEnumerable<BidDTO>> GetAll()
        {
            var bids = _biddingService.GetAll();
            return Ok(bids);
        }

        [HttpGet("{id}")]
        public ActionResult<BidDTO> GetById(int id)
        {
            var bid = _biddingService.GetById(id);
            if (bid == null)
            {
                return NotFound();
            }
            return Ok(bid);
        }

        [HttpGet("lot/{lotId}")]
        public ActionResult<IEnumerable<BidDTO>> GetBidsByLotId(int lotId)
        {
            var bids = _biddingService.GetBidsByLotId(lotId);
            if (bids == null || !bids.Any())
            {
                return NotFound();
            }
            return Ok(bids);
        }

        [HttpPost]
        public ActionResult<int> PlaceBid(int lotId, int userId, decimal amount)
        {
            var id = _biddingService.PlaceBid(lotId, userId, amount);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}
