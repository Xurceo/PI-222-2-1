using DAL.Models;

namespace BLL.DTOs
{
    public class BidDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }
        public required UserDTO User { get; set; }
        public required LotDTO Lot { get; set; }
    }
}
