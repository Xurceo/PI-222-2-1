using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Lot
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal StartPrice { get; set; }

        public bool IsConfirmed { get; set; } = false;

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
