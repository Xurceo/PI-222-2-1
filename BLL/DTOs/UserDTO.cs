namespace BLL.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = "USER";
<<<<<<< HEAD
        public IEnumerable<Guid> LotIds { get; set; } = [];
        public IEnumerable<Guid> BidIds { get; set; } = [];
=======
        public ICollection<Guid> LotIds { get; set; } = [];
        public ICollection<Guid> BidIds { get; set; } = [];
>>>>>>> 15e7ec842fee30f522e1a47cda5b4560e39ffc52
    }
}
