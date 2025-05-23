namespace BLL.Models
{
    public class BidDTO
    {
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }
        public UserDTO User { get; set; } = null!;
        public LotDTO Lot { get; set; } = null!;
    }
}
