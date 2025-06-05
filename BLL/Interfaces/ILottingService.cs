using BLL.CreateDTOs;
using BLL.DTOs;
using BLL.ShortDTOs;
using DAL.Enums;

namespace BLL.Interfaces
{
    public interface ILottingService
    {
        Task<IEnumerable<LotDTO>> GetAll();
        Task<IEnumerable<LotShortDTO>> GetAllShort();

        Task<IEnumerable<LotDTO>> GetAllByCategoryId(Guid categoryId);
        Task<LotShortDTO?> GetById(Guid id);
        Task<Guid> AddLot(CreateLotDTO lot);
        Task UpdateLot(LotDTO lot);
        Task DeleteLot(Guid id);
        Task ChangeLotStatus(Guid lotId, LotStatus status);
    }
}
