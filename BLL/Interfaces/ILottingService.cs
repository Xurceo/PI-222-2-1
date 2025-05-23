using BLL.Models;

namespace BLL.Interfaces
{
    public interface ILottingService
    {
        IEnumerable<LotDTO> GetAll();
        IEnumerable<LotDTO> GetAllByCategoryId(int categoryId);
        LotDTO? GetById(int id);
        int AddLot(LotDTO lot);
        void UpdateLot(LotDTO lot);
        void DeleteLot(int id);
    }
}
