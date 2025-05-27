using AutoMapper;
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
        public void DeleteBid(Guid id)
        {
            _unitOfWork.Bids.Delete(id);
            _unitOfWork.Bids.Save();
        }

        public BidDTO? GetById(Guid id)
        {
            var bid = _unitOfWork.Bids.GetById(id);
            return bid != null ? _mapper.Map<BidDTO>(bid) : null;
        }

        public IEnumerable<BidDTO> GetAll()
        {
            var bids = _unitOfWork.Bids.GetAll();
            return _mapper.Map<IEnumerable<BidDTO>>(bids);
        }

        public IEnumerable<BidDTO> GetBidsByLotId(Guid lotId)
        {
            var lot = _unitOfWork.Lots.GetById(lotId);
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

        public Guid PlaceBid(BidDTO dto)
        {
            var lot = _unitOfWork.Lots.GetById(dto.Lot.Id);
            var user = _unitOfWork.Users.GetById(dto.User.Id);
            if (lot is null)
            {
                throw new ArgumentException("Lot not found");
            }
            if (user is null)
            {
                throw new ArgumentException("User not found");
            }

            var bid = _mapper.Map<Bid>(dto);
            bid.Time = DateTime.UtcNow;
            bid.Lot = lot;
            bid.User = user;

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
            if (lot.WinnerId.HasValue && lot.WinnerId.Value == user.Id)
            {
                throw new InvalidOperationException("You cannot bid on your own lot");
            }
            if (lot.OwnerId == user.Id)
            {
                throw new InvalidOperationException("You cannot bid on your own lot");
            }
            if (lot.WinnerId.HasValue && lot.WinnerId.Value != user.Id)
            {
                throw new InvalidOperationException("You cannot bid on a lot that already has a winner");
            }
            if (lot.StartPrice > bid.Amount)
            {
                throw new ArgumentException("Bid amount must be greater than the starting price of the lot");
            }

            lot.Bids.Add(bid);
            user.Bids.Add(bid);

            lot.WinnerId = lot.Bids.OrderByDescending(b => b.Amount).FirstOrDefault()?.UserId;
            if (lot.WinnerId.HasValue)
            {
                lot.Winner = _unitOfWork.Users.GetById(lot.WinnerId.Value);
            }
            else
            {
                lot.Winner = null;
            }

            _unitOfWork.Bids.Create(bid);
            _unitOfWork.Lots.Update(lot);
            _unitOfWork.Users.Update(user);
            _unitOfWork.Bids.Save();
            return bid.Id;
        }
    }
}
