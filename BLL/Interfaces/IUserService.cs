using BLL.CreateDTOs;
using BLL.DTOs;
using BLL.ShortDTOs;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<Guid> AddUser(CreateUserDTO dto);
        Task<UserDTO?> GetById(Guid id);
        Task<IEnumerable<UserGetDTO>> GetAll();
        Task<IEnumerable<UserShortDTO>> GetAllShort();
        Task UpdateUser(UserDTO dto);
        Task DeleteUser(Guid id);
        Task UpdateUserPassword(Guid id, string newPassword);
        Task<UserDTO> Authenticate(LoginDTO dto);
    }
}
