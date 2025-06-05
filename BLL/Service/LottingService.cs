using AutoMapper;
using BLL.Interfaces;
using BLL.DTOs;
using BLL.CreateDTOs;
using DAL.Models;
using DAL.UoW;
using BLL.Exceptions;
using DAL.Enums;
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
            if (dto.OwnerId == null)
            {
                throw new ArgumentException("OwnerId cannot be null");
            }
            var owner = await _unitOfWork.Users.GetById(dto.OwnerId.Value);
            if (owner == null)
            {
                throw new NotFoundException(owner);
            }
            var category = await _unitOfWork.Categories.GetById(dto.CategoryId);
            if (category == null)
            {
                throw new NotFoundException(category);
            }

            lot.StartTime = DateTime.UtcNow;

            await _unitOfWork.Lots.Create(lot);
            await _unitOfWork.Lots.Save();

            return lot.Id;
        }

        public async Task ChangeLotStatus(Guid lotId, LotStatus status)
        {
            var lot = await _unitOfWork.Lots.GetById(lotId);
            if (lot == null)
            {
                throw new NotFoundException(lot);
            }
            lot.Status = status;
            if (status == LotStatus.Sold)
            {
                lot.Winner = lot.Bids.MaxBy(b => b.Amount)!.User;
            }
            await _unitOfWork.Lots.Update(lot);
            await _unitOfWork.Lots.Save();
        }

        public async Task DeleteLot(Guid id)
        {
            _unitOfWork.Lots.Delete(id);
            await _unitOfWork.Lots.Save();
        }

        public async Task<IEnumerable<LotDTO>> GetAll()
        {
            var lots = await _unitOfWork.Lots.GetAll(l => l.Category, l => l.Owner, l => l.Bids);
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
        public async Task<IEnumerable<BidDTO>> GetLotBids(Guid lotId)
        {
            var lot = await _unitOfWork.Lots.GetById(lotId);
            if (lot is not null)
            {
                var bids = lot.Bids;
                return _mapper.Map<IEnumerable<BidDTO>>(bids);
            }
            else
            {
                throw new ArgumentException("Lot not found");
            }
        }

        public async Task UpdateLot(LotDTO dto)
        {
            var lot = await _unitOfWork.Lots.GetById(dto.Id);
            if (lot == null)
            {
                throw new NotFoundException(lot);
            }

            _mapper.Map(dto, lot);
            await _unitOfWork.Lots.Update(lot);
            await _unitOfWork.Lots.Save();
        }
    }
}