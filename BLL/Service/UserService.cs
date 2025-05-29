using AutoMapper;
using BLL.Interfaces;
using BLL.DTOs;
using BLL.Utility;
using DAL.Models;
using DAL.UoW;
using BLL.CreateDTOs;

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
            var users = await _unitOfWork.Users.GetAll(u => u.Lots, u => u.Bids);
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetById(Guid id)
        {
            var users = await _unitOfWork.Users.GetAll(u => u.Lots, u => u.Bids);
            var user = users.FirstOrDefault(u => u.Id == id);
            return user != null ? _mapper.Map<UserDTO>(user) : null;
        }

        public async Task UpdateUser(UserDTO dto)
        {
            var user = await _unitOfWork.Users.GetById(dto.Id);
            if (user == null)
                throw new Exception("User not found");

            _mapper.Map(dto, user);

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