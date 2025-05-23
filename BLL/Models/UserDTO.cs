namespace BLL.Models
{
    public class UserDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public ICollection<LotDTO> Lots { get; set; } = [];
        public ICollection<BidDTO> Bids { get; set; } = [];
    }
}
