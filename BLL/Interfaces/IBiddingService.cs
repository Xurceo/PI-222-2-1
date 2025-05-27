using BLL.DTOs;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IBiddingService
    {
        Guid PlaceBid(BidDTO dto);
        IEnumerable<BidDTO> GetBidsByLotId(Guid lotId);
        BidDTO? GetById(Guid id);
        IEnumerable<BidDTO> GetAll();
    }
}
