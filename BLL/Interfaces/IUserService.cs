using BLL.Models;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        int AddUser(CreateUserDTO dto);
        UserDTO? GetById(int id);
        IEnumerable<UserDTO> GetAll();
        void UpdateUser(UserDTO dto);
        void DeleteUser(int id);
        void UpdateUserPassword(int id, string newPassword);
    }
}
