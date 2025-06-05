namespace BLL.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = "USER";
        public IEnumerable<Guid> LotIds { get; set; } = [];
        public IEnumerable<Guid> BidIds { get; set; } = [];
    }
}
