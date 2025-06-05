using BLL.CreateDTOs;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IBiddingService
    {
        Task<Guid> PlaceBid(Guid userId, Guid lotId, decimal amount);
<<<<<<< HEAD
        Task<IEnumerable<BidDTO>> GetLotBids(Guid lotId);
=======
>>>>>>> 15e7ec842fee30f522e1a47cda5b4560e39ffc52
        Task<BidDTO?> GetById(Guid id);
        Task<IEnumerable<BidDTO>> GetAll();
        Task DeleteBid(Guid id);
    }
}