using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Utility;
using DAL.Models;
using DAL.UoW;

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

        public int AddUser(CreateUserDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            user.PasswordHash = PasswordHelper.HashPassword(dto.Password); // custom logic

            _unitOfWork.Users.Create(user);
            _unitOfWork.Users.Save();

            return user.Id;
        }

        public void DeleteUser(int id)
        {
            _unitOfWork.Users.Delete(id);
            _unitOfWork.Users.Save();
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var users = _unitOfWork.Users.GetAll();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public UserDTO? GetById(int id)
        {
            var user = _unitOfWork.Users.GetById(id);
            return user != null ? _mapper.Map<UserDTO>(user) : null;
        }

        public void UpdateUser(UserDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            _unitOfWork.Users.Update(user);
            _unitOfWork.Users.Save();
        }

        public void UpdateUserPassword(int id, string newPassword)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user != null)
            {
                user.PasswordHash = PasswordHelper.HashPassword(newPassword); // custom logic
                _unitOfWork.Users.Update(user);
                _unitOfWork.Users.Save();
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }
}
