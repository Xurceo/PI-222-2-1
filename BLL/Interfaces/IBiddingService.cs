using BLL.Models;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IBiddingService
    {
        int PlaceBid(int lotId, int userId, decimal amount);
        IEnumerable<BidDTO> GetBidsByLotId(int lotId);
        BidDTO? GetById(int id);
        IEnumerable<BidDTO> GetAll();
    }
}
