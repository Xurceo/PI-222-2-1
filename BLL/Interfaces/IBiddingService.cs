using System.Collections.Generic;

namespace BLL.Interfaces
{
    interface IBiddingService
    {
        IEnumerable<BidDTO> GetBidsForAuction(int itemId);
        BidDTO GetWinningBid(int itemId);
    }
}
