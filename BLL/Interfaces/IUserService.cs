using BLL.CreateDTOs;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<Guid> AddUser(CreateUserDTO dto);
        Task<UserDTO?> GetById(Guid id);
        Task<IEnumerable<BidDTO>> GetUserBids(Guid userId);
        Task<IEnumerable<LotDTO>> GetUserLots(Guid userId);
        Task<IEnumerable<UserDTO>> GetAll();
        Task UpdateUser(UserDTO dto);
        Task DeleteUser(Guid id);
        Task UpdateUserPassword(Guid id, string newPassword);
        Task<UserDTO> Authenticate(LoginDTO dto);
    }
}
