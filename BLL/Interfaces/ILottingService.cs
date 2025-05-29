using BLL.CreateDTOs;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface ILottingService
    {
        Task<IEnumerable<LotDTO>> GetAll();
        Task<IEnumerable<LotDTO>> GetAllByCategoryId(Guid categoryId);
        Task<LotDTO?> GetById(Guid id);
        Task<Guid> AddLot(CreateLotDTO lot);
        Task UpdateLot(LotDTO lot);
        Task DeleteLot(Guid id);
        Task ConfirmLot(Guid lotId);
    }
}
