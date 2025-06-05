namespace BLL.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = "USER";
        public ICollection<Guid> LotIds { get; set; } = [];
        public ICollection<Guid> BidIds { get; set; } = [];
    }
}
