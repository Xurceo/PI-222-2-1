using BLL.DTOs;
using DAL.Models;

namespace BLL.DTOs
{
    public class LotDTO
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal StartPrice { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsEnded { get; set; }
        public required UserDTO Owner { get; set; }
        public UserDTO? Winner { get; set; }
        public required CategoryDTO Category { get; set; }
        public ICollection<BidDTO> Bids { get; set; } = [];
    }
}
