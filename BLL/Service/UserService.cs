using AutoMapper;
using BLL.Interfaces;
using BLL.DTOs;
using BLL.Utility;
using DAL.Models;
using DAL.UoW;
using BLL.CreateDTOs;
using BLL.ShortDTOs;

namespace BLL.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> AddUser(CreateUserDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            user.PasswordHash = PasswordHelper.HashPassword(dto.Password);

            await _unitOfWork.Users.Create(user);
            await _unitOfWork.Users.Save();

            return user.Id;
        }

        public async Task DeleteUser(Guid id)
        {
            _unitOfWork.Users.Delete(id);
            await _unitOfWork.Users.Save();
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var users = await _unitOfWork.Users.GetAll();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }
        public async Task<IEnumerable<UserShortDTO>> GetAllShort()
        {
            var users = await _unitOfWork.Users.GetAll();
            return _mapper.Map<IEnumerable<UserShortDTO>>(users);
        }
        public async Task<UserDTO?> GetById(Guid id)
        {
            var users = await _unitOfWork.Users.GetAll();
            var user = users.FirstOrDefault(u => u.Id == id);
            return user != null ? _mapper.Map<UserDTO>(user) : null;
        }

        public async Task UpdateUser(UserDTO dto)
        {
            var user = await _unitOfWork.Users.GetById(dto.Id);

            ArgumentNullException.ThrowIfNull(user, "User not found");

            _mapper.Map(dto, user);

            if (dto.BidIds != null)
            {
                // Get existing bids to remove (those not in the new list)
                var bidsToRemove = user.Bids
                    .Where(b => !dto.BidIds.Contains(b.Id))
                    .ToList();
                // Get bids to add (those not already in the collection)
                var existingBidIds = user.Bids.Select(b => b.Id).ToList();
                var bidsToAddIds = dto.BidIds.Except(existingBidIds);
                // Remove bids
                foreach (var bid in bidsToRemove)
                {
                    user.Bids.Remove(bid);
                }
                // Add new bids
                foreach (var bidId in bidsToAddIds)
                {
                    var bid = await _unitOfWork.Bids.GetById(bidId);
                    if (bid != null)
                    {
                        user.Bids.Add(bid);
                    }
                }
            }

            if (dto.LotIds != null)
            {
                // Get existing lots to remove (those not in the new list)
                var lotsToRemove = user.Lots
                    .Where(l => !dto.LotIds.Contains(l.Id))
                    .ToList();
                // Get lots to add (those not already in the collection)
                var existingLotIds = user.Lots.Select(l => l.Id).ToList();
                var lotsToAddIds = dto.LotIds.Except(existingLotIds);
                // Remove lots
                foreach (var lot in lotsToRemove)
                {
                    user.Lots.Remove(lot);
                }
                // Add new lots
                foreach (var lotId in lotsToAddIds)
                {
                    var lot = await _unitOfWork.Lots.GetById(lotId);
                    if (lot != null)
                    {
                        user.Lots.Add(lot);
                    }
                }
            }

            await _unitOfWork.Users.Update(user);
            await _unitOfWork.Users.Save();
        }

        public async Task UpdateUserPassword(Guid id, string newPassword)
        {
            var user = await _unitOfWork.Users.GetById(id);
            if (user != null)
            {
                user.PasswordHash = PasswordHelper.HashPassword(newPassword);
                await _unitOfWork.Users.Update(user);
                await _unitOfWork.Users.Save();
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public async Task<UserDTO> Authenticate(LoginDTO dto)
        {
            // Шукаємо користувача по username
            var users = await _unitOfWork.Users.GetAll();
            var user = users.FirstOrDefault(u => u.Username == dto.Username);

            if (user == null)
                throw new Exception("User not found");

            // Перевіряємо пароль
            if (!PasswordHelper.VerifyPassword(dto.Password, user.PasswordHash))
                throw new Exception("Invalid password");

            // Мапимо і повертаємо UserDTO (без пароля)
            return _mapper.Map<UserDTO>(user);
        }
    }
}