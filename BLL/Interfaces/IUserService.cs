using BLL.Models;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        void CreateUser(CreateUserDTO dto);
        UserDTO? GetById(int id);
        IEnumerable<UserDTO> GetAll();
        void UpdateUser(UserDTO dto);
        void DeleteUser(int id);
        void UpdateUserPassword(int id, string newPassword);
    }
}
