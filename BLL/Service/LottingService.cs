using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Models;
using DAL.UoW;

namespace BLL.Service
{
    public class LottingService : ILottingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public LottingService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public int AddLot(LotDTO dto)
        {
            var lot = _mapper.Map<Lot>(dto);

            _unitOfWork.Lots.Create(lot);
            _unitOfWork.Save();

            return lot.Id;
        }
        public void DeleteLot(int id)
        {
            _unitOfWork.Lots.Delete(id);
            _unitOfWork.Save();
        }
        public IEnumerable<LotDTO> GetAll()
        {
            var lots = _unitOfWork.Lots.GetAll();
            return _mapper.Map<IEnumerable<LotDTO>>(lots);
        }

        public IEnumerable<LotDTO> GetAllByCategoryId(int categoryId)
        {
            var lots = _unitOfWork.Lots.GetAll().Where(l => l.CategoryId == categoryId);
            return _mapper.Map<IEnumerable<LotDTO>>(lots);
        }
        public LotDTO? GetById(int id)
        {
            var lot = _unitOfWork.Lots.GetById(id);

            return lot != null ? _mapper.Map<LotDTO>(lot) : null;
        }
        public void UpdateLot(LotDTO dto)
        {
            var lot = _mapper.Map<Lot>(dto);
            _unitOfWork.Lots.Update(lot);
            _unitOfWork.Save();
        }
    }
}
