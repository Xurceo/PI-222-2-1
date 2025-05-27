using BLL.CreateDTOs;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Guid AddUser(CreateUserDTO dto);
        UserDTO? GetById(Guid id);
        IEnumerable<UserDTO> GetAll();
        void UpdateUser(UserDTO dto);
        void DeleteUser(Guid id);
        void UpdateUserPassword(Guid id, string newPassword);
    }
}
