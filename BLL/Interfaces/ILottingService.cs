using BLL.CreateDTOs;
using BLL.DTOs;
using DAL.Enums;

namespace BLL.Interfaces
{
    public interface ILottingService
    {
        Task<IEnumerable<LotDTO>> GetAll();
        Task<IEnumerable<BidDTO>> GetLotBids(Guid lotId);
        Task<LotDTO?> GetById(Guid id);
        Task<Guid> AddLot(CreateLotDTO lot);
        Task UpdateLot(LotDTO lot);
        Task DeleteLot(Guid id);
        Task ChangeLotStatus(Guid lotId, LotStatus status);
    }
}
