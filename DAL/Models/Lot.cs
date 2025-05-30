using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DAL.Enums;

namespace DAL.Models
{
    public class Lot
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal StartPrice { get; set; }
        public LotStatus Status { get; set; } = LotStatus.Pending;
        public bool IsEnded { get; set; } = false;
        public DateTime? EndTime { get; set; }
        public DateTime? StartTime { get; set; }

        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }
        public required Category Category { get; set; }

        [ForeignKey("OwnerId")]
        public Guid OwnerId { get; set; }
        public required User Owner { get; set; }

        [ForeignKey("WinnerId")]
        public Guid? WinnerId { get; set; }
        public User? Winner { get; set; }
        public ICollection<Bid> Bids { get; set; } = new List<Bid>();
    }

}
