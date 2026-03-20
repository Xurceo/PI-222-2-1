using AutoMapper;
using BLL.Interfaces;
using BLL.DTOs;
using BLL.Utility;
using DAL.Models;
using DAL.UoW;
using BLL.CreateDTOs;
using BLL.Exceptions;

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

        public async Task<Guid> AddUser(CreateUserDTO user)
        {
            var mappedUser = _mapper.Map<User>(user);
            mappedUser.PasswordHash = PasswordHelper.HashPassword(user.Password);

            await _unitOfWork.Users.Create(mappedUser);
            await _unitOfWork.Users.Save();

            return mappedUser.Id;
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
        public async Task<IEnumerable<BidDTO>> GetUserBids(Guid userId)
        {
            var user = await _unitOfWork.Users.GetById(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            return _mapper.Map<IEnumerable<BidDTO>>(user.Bids);
        }
        public async Task<IEnumerable<LotDTO>> GetUserLots(Guid userId)
        {
            var user = await _unitOfWork.Users.GetById(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            return _mapper.Map<IEnumerable<LotDTO>>(user.Lots);
        }
        public async Task<UserDTO?> GetById(Guid id)
        {
            var users = await _unitOfWork.Users.GetAll();
            var user = users.FirstOrDefault(u => u.Id == id);
            return user != null ? _mapper.Map<UserDTO>(user) : null;
        }

        public async Task UpdateUser(UserDTO user)
        {
            var mappedUser = await _unitOfWork.Users.GetById(user.Id);

            ArgumentNullException.ThrowIfNull(mappedUser, "User not found");

            _mapper.Map(user, mappedUser);

            if (user.BidIds != null)
            {
                await SyncBids(mappedUser, user.BidIds);
            }

            if (user.LotIds != null)
            {
                await SyncLots(mappedUser, user.LotIds);
            }

            await _unitOfWork.Users.Update(mappedUser);
            await _unitOfWork.Users.Save();
        }

        private async Task SyncBids(User mappedUser, IEnumerable<Guid> bidIds)
        {
            // Удаляем ставки, которых нет в новом списке
            var bidsToRemove = mappedUser.Bids
                .Where(b => !bidIds.Contains(b.Id))
                .ToList();

            foreach (var bid in bidsToRemove)
            {
                mappedUser.Bids.Remove(bid);
            }

            // Добавляем новые ставки, которых ещё нет в коллекции
            var existingBidIds = mappedUser.Bids.Select(b => b.Id).ToHashSet();
            var bidsToAddIds = bidIds.Except(existingBidIds);

            foreach (var bidId in bidsToAddIds)
            {
                var bid = await _unitOfWork.Bids.GetById(bidId);
                if (bid != null)
                {
                    mappedUser.Bids.Add(bid);
                }
            }
        }

        private async Task SyncLots(User mappedUser, IEnumerable<Guid> lotIds)
        {
            // Удаляем лоты, которых нет в новом списке
            var lotsToRemove = mappedUser.Lots
                .Where(l => !lotIds.Contains(l.Id))
                .ToList();

            foreach (var lot in lotsToRemove)
            {
                mappedUser.Lots.Remove(lot);
            }

            // Добавляем новые лоты, которых ещё нет в коллекции
            var existingLotIds = mappedUser.Lots.Select(l => l.Id).ToHashSet();
            var lotsToAddIds = lotIds.Except(existingLotIds);

            foreach (var lotId in lotsToAddIds)
            {
                var lot = await _unitOfWork.Lots.GetById(lotId);
                if (lot != null)
                {
                    mappedUser.Lots.Add(lot);
                }
            }
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
                throw new NotFoundException(typeof(User));
            }
        }

        public async Task<UserDTO> Authenticate(LoginDTO user)
        {
            // Шукаємо користувача по username
            var users = await _unitOfWork.Users.GetAll();
            var dbUser = users.FirstOrDefault(u => u.Username == user.Username);

            if (dbUser == null)
                throw new NotFoundException(typeof(User));

            // Перевіряємо пароль
            if (!PasswordHelper.VerifyPassword(user.Password, dbUser.PasswordHash))
                throw new ArgumentException("Invalid password");

            // Мапимо і повертаємо UserDTO (без пароля)
            return _mapper.Map<UserDTO>(dbUser);
        }
    }
}