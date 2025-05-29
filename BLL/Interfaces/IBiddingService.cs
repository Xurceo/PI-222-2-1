using BLL.DTOs;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IBiddingService
    {
        Task<Guid> PlaceBid(BidDTO dto);
        Task<IEnumerable<BidDTO>> GetBidsByLotId(Guid lotId);
        Task<BidDTO?> GetById(Guid id);
        Task<IEnumerable<BidDTO>> GetAll();
        Task DeleteBid(Guid id);
    }
}