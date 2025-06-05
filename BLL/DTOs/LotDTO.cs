using DAL.Enums;

namespace BLL.DTOs
{
    public class LotDTO
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal StartPrice { get; set; }
        public LotStatus Status { get; set; } = LotStatus.Pending;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public required Guid OwnerId { get; set; }
        public Guid? WinnerId { get; set; }
        public required Guid CategoryId { get; set; }
<<<<<<< HEAD
        public IEnumerable<Guid> BidIds { get; set; } = [];
=======
        public ICollection<Guid> BidIds { get; set; } = [];
>>>>>>> 15e7ec842fee30f522e1a47cda5b4560e39ffc52
    }
}
