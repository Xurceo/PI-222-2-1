namespace BLL.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public ICollection<LotDTO> Lots { get; set; } = [];
        public ICollection<BidDTO> Bids { get; set; } = [];
    }
}
