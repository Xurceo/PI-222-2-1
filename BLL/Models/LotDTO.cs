namespace BLL.Models
{
    public class LotDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal StartPrice { get; set; }
        public bool IsConfirmed { get; set; }
        public UserDTO Winner => Bids.OrderByDescending(b => b.Amount).FirstOrDefault()?.User ?? Owner;
        public UserDTO Owner { get; set; } = null!;
        public CategoryDTO Category { get; set; } = null!;
        public ICollection<BidDTO> Bids { get; set; } = [];
    }
}
