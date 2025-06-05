using BLL.CreateDTOs;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<Guid> AddUser(CreateUserDTO dto);
        Task<UserDTO?> GetById(Guid id);
<<<<<<< HEAD
=======
        Task<IEnumerable<BidDTO>> GetUserBids(Guid userId);
        Task<IEnumerable<LotDTO>> GetUserLots(Guid userId);
>>>>>>> 15e7ec842fee30f522e1a47cda5b4560e39ffc52
        Task<IEnumerable<UserDTO>> GetAll();
        Task UpdateUser(UserDTO dto);
        Task DeleteUser(Guid id);
        Task UpdateUserPassword(Guid id, string newPassword);
        Task<UserDTO> Authenticate(LoginDTO dto);
    }
}
