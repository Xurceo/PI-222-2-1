using BLL.CreateDTOs;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<Guid> AddUser(CreateUserDTO dto);
        Task<UserDTO?> GetById(Guid id);
        Task<IEnumerable<UserDTO>> GetAll();
        Task UpdateUser(UserDTO dto);
        Task DeleteUser(Guid id);
        Task UpdateUserPassword(Guid id, string newPassword);
    }
}
