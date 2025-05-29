using AutoMapper;
using BLL.CreateDTOs;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Models;
using DAL.UoW;

namespace BLL.Service
{
    public class BiddingService : IBiddingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public BiddingService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task DeleteBid(Guid id)
        {
            _unitOfWork.Bids.Delete(id);
            await _unitOfWork.Bids.Save();
        }

        public async Task<BidDTO?> GetById(Guid id)
        {
            var bid = await _unitOfWork.Bids.GetById(id);
            return bid != null ? _mapper.Map<BidDTO>(bid) : null;
        }

        public async Task<IEnumerable<BidDTO>> GetAll()
        {
            var bids = await _unitOfWork.Bids.GetAll();
            return _mapper.Map<IEnumerable<BidDTO>>(bids);
        }

        public async Task<IEnumerable<BidDTO>> GetBidsByLotId(Guid lotId)
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

        public async Task<Guid> PlaceBid(Guid userId, Guid lotId, decimal amount)
        {
            var lot = await _unitOfWork.Lots.GetById(lotId);
            var user = await _unitOfWork.Users.GetById(userId);
            if (lot is null)
            {
                throw new ArgumentException("Lot not found");
            }
            if (user is null)
            {
                throw new ArgumentException("User not found");
            }

            var bid = new Bid
            {
                Id = Guid.NewGuid(),
                Amount = amount,
                Time = DateTime.UtcNow,
                UserId = user.Id,
                LotId = lot.Id,
                Lot = lot,
                User = user
            };

            if (lot.Bids.Count != 0)
            {
                var lastBid = lot.Bids.OrderByDescending(b => b.Time).First();
                if (bid.Amount <= lastBid.Amount)
                {
                    throw new ArgumentException("Bid amount must be greater than the last bid");
                }
            }
            if (lot.IsConfirmed == false)
            {
                throw new InvalidOperationException("Cannot place a bid on an unconfirmed lot");
            }
            if (lot.OwnerId == user.Id)
            {
                throw new InvalidOperationException("You cannot bid on your own lot");
            }
            if (lot.StartPrice > bid.Amount)
            {
                throw new ArgumentException("Bid amount must be greater than the starting price of the lot");
            }

            await _unitOfWork.Bids.Create(bid);
            await _unitOfWork.Bids.Save();
            return bid.Id;
        }
    }
}
