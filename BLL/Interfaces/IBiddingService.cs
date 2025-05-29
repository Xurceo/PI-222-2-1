using BLL.CreateDTOs;
using BLL.DTOs;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IBiddingService
    {
        Task<Guid> PlaceBid(Guid userId, Guid lotId, decimal amount);
        Task<IEnumerable<BidDTO>> GetBidsByLotId(Guid lotId);
        Task<BidDTO?> GetById(Guid id);
        Task<IEnumerable<BidDTO>> GetAll();
        Task DeleteBid(Guid id);
    }
}