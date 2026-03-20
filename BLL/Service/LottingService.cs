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

        public async Task<Guid> AddLot(CreateLotDTO lot)
        {
            var mappedLot = _mapper.Map<Lot>(lot);
            if (lot.OwnerId == null)
            {
                throw new ArgumentException("OwnerId cannot be null");
            }
            var owner = await _unitOfWork.Users.GetById(lot.OwnerId.Value);
            if (owner == null)
            {
                throw new NotFoundException(typeof(User));
            }
            var category = await _unitOfWork.Categories.GetById(lot.CategoryId);
            if (category == null)
            {
                throw new NotFoundException(typeof(Category));
            }

            mappedLot.StartTime = DateTime.UtcNow;

            await _unitOfWork.Lots.Create(mappedLot);
            await _unitOfWork.Lots.Save();

            return mappedLot.Id;
        }

        public async Task ChangeLotStatus(Guid lotId, LotStatus status)
        {
            var lot = await _unitOfWork.Lots.GetById(lotId);
            if (lot == null)
            {
                throw new NotFoundException(typeof(Lot));
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

        public async Task UpdateLot(LotDTO lot)
        {
            var dbLot = await _unitOfWork.Lots.GetById(lot.Id);

            ArgumentNullException.ThrowIfNull(dbLot, "Lot not found");

            _mapper.Map(lot, dbLot);

            if (lot.BidIds != null)
            {
                // Get existing bids to remove (those not in the new list)
                var bidsToRemove = dbLot.Bids
                    .Where(b => !lot.BidIds.Contains(b.Id))
                    .ToList();
                // Get bids to add (those not already in the collection)
                var existingBidIds = dbLot.Bids.Select(b => b.Id).ToList();
                var bidsToAddIds = lot.BidIds.Except(existingBidIds);
                // Remove bids
                foreach (var bid in bidsToRemove)
                {
                    dbLot.Bids.Remove(bid);
                }
                // Add new bids
                foreach (var bidId in bidsToAddIds)
                {
                    var bid = await _unitOfWork.Bids.GetById(bidId);
                    if (bid != null)
                    {
                        dbLot.Bids.Add(bid);
                    }
                }
            }

            await _unitOfWork.Lots.Update(dbLot);
            await _unitOfWork.Lots.Save();
        }
    }
}