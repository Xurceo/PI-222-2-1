using BLL.CreateDTOs;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface ILottingService
    {
        IEnumerable<LotDTO> GetAll();
        IEnumerable<LotDTO> GetAllByCategoryId(Guid categoryId);
        LotDTO? GetById(Guid id);
        Guid AddLot(CreateLotDTO lot);
        void UpdateLot(LotDTO lot);
        void DeleteLot(Guid id);
    }
}
