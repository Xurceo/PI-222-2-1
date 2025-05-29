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

        public async Task<Guid> AddLot(CreateLotDTO dto)
        {
            var lot = _mapper.Map<Lot>(dto);
            var owner = await _unitOfWork.Users.GetById(dto.OwnerId);
            if (owner == null)
            {
                throw new ArgumentException($"User with id {dto.OwnerId} not found");
            }
            var category = await _unitOfWork.Categories.GetById(dto.CategoryId);
            if (category == null)
            {
                throw new ArgumentException($"Category with id {dto.CategoryId} not found");
            }

            await _unitOfWork.Lots.Create(lot);
            await _unitOfWork.Lots.Save();

            return lot.Id;
        }

        public async Task DeleteLot(Guid id)
        {
            _unitOfWork.Lots.Delete(id);
            await _unitOfWork.Lots.Save();
        }

        public async Task<IEnumerable<LotDTO>> GetAll()
        {
            var lots = await _unitOfWork.Lots.GetAll();
            return _mapper.Map<IEnumerable<LotDTO>>(lots);
        }

        public async Task<IEnumerable<LotDTO>> GetAllByCategoryId(Guid categoryId)
        {
            var lots = await _unitOfWork.Lots.GetAll();
            var filtered = lots.Where(l => l.CategoryId == categoryId);
            return _mapper.Map<IEnumerable<LotDTO>>(filtered);
        }

        public async Task<LotDTO?> GetById(Guid id)
        {
            var lot = await _unitOfWork.Lots.GetById(id);
            return lot != null ? _mapper.Map<LotDTO>(lot) : null;
        }

        public async Task UpdateLot(LotDTO dto)
        {
            var lot = await _unitOfWork.Lots.GetById(dto.Id);
            if (lot == null)
            {
                throw new ArgumentException($"Lot with id {dto.Id} not found");
            }

            _mapper.Map(dto, lot);
            await _unitOfWork.Lots.Update(lot);
            await _unitOfWork.Lots.Save();
        }
    }
}