using DAL.Models;

namespace BLL.DTOs
{
    public class BidDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }
        public required Guid UserId { get; set; }
        public required Guid LotId { get; set; }
    }
}
