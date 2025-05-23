using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
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
        public void DeleteBid(int id)
        {
            _unitOfWork.Bids.Delete(id);
            _unitOfWork.Bids.Save();
        }

        public BidDTO? GetById(int id)
        {
            var bid = _unitOfWork.Bids.GetById(id);
            return bid != null ? _mapper.Map<BidDTO>(bid) : null;
        }

        public IEnumerable<BidDTO> GetAll()
        {
            var bids = _unitOfWork.Bids.GetAll();
            return _mapper.Map<IEnumerable<BidDTO>>(bids);
        }

        public IEnumerable<BidDTO> GetBidsByLotId(int lotId)
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

        public int PlaceBid(int lotId, int userId, decimal amount)
        {
            var lot = _unitOfWork.Lots.GetById(lotId);
            var user = _unitOfWork.Users.GetById(userId);

            if (lot is not null && user is not null)
            {
                var bid = new Bid
                {
                    Lot = lot,
                    LotId = lot.Id,
                    User = user,
                    UserId = user.Id,
                    Amount = amount,
                    Time = DateTime.Now
                };

                _unitOfWork.Bids.Create(bid);
                _unitOfWork.Bids.Save();

                return bid.Id;
            }

            throw new ArgumentException("Lot or User not found");
        }
    }
}
