using BLL.Models;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        void CreateUser(CreateUserDTO dto);
        UserDTO? GetUserById(int id);
        IEnumerable<UserDTO> GetAllUsers();
        void UpdateUser(UserDTO dto);
        void DeleteUser(int id);
        void UpdateUserPassword(int id, string newPassword);
    }
}
