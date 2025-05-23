using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Lot
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal StartPrice { get; set; }

        public bool IsConfirmed { get; set; } = false;

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public required Category Category { get; set; }

        public int OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public required User Owner { get; set; }

        public int? WinnerId { get; set; }
        [ForeignKey("WinnerId")]
        public User? Winner { get; set; }
        public ICollection<Bid> Bids { get; set; } = new List<Bid>();
    }

}
