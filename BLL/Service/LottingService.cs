using AutoMapper;
using BLL.Interfaces;
using BLL.DTOs;
using BLL.CreateDTOs;
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
        public Guid AddLot(CreateLotDTO dto)
        {
            var lot = _mapper.Map<Lot>(dto);
            var owner = _unitOfWork.Users.GetById(dto.OwnerId);
            if (owner == null)
            {
                throw new ArgumentException($"User with id {dto.OwnerId} not found");
            }
            var category = _unitOfWork.Categories.GetById(dto.CategoryId);
            if (category == null)
            {
                throw new ArgumentException($"Category with id {dto.CategoryId} not found");
            }

            _unitOfWork.Lots.Create(lot);
            _unitOfWork.Save();

            return lot.Id;
        }
        public void DeleteLot(Guid id)
        {
            _unitOfWork.Lots.Delete(id);
            _unitOfWork.Save();
        }
        public IEnumerable<LotDTO> GetAll()
        {
            var lots = _unitOfWork.Lots.GetAll();
            return _mapper.Map<IEnumerable<LotDTO>>(lots);
        }

        public IEnumerable<LotDTO> GetAllByCategoryId(Guid categoryId)
        {
            var lots = _unitOfWork.Lots.GetAll().Where(l => l.CategoryId == categoryId);
            return _mapper.Map<IEnumerable<LotDTO>>(lots);
        }
        public LotDTO? GetById(Guid id)
        {
            var lot = _unitOfWork.Lots.GetById(id);
            return lot != null ? _mapper.Map<LotDTO>(lot) : null;
        }
        public void UpdateLot(LotDTO dto)
        {
            var lot = _unitOfWork.Lots.GetById(dto.Id);
            if (lot == null)
            {
                throw new ArgumentException($"Lot with id {dto.Id} not found");
            }

            _mapper.Map(dto, lot);
            _unitOfWork.Lots.Update(lot);
            _unitOfWork.Save();
        }
    }
}
