using BLL.Models;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    interface IBiddingService
    {
        void PlaceBid(int lotId, int userId, decimal amount);
        IEnumerable<BidDTO> GetBidsByLotId(int lotId);
    }
}
