using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Bid
    {
        [Key]
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime Time { get; set; }

        [ForeignKey("LotId")]
        public Guid LotId { get; set; }
        public required Lot Lot { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public required User User { get; set; }
    }
}
