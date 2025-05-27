using System.ComponentModel.DataAnnotations;


namespace DAL.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "USER"; 

        public ICollection<Lot> Lots { get; set; } = new List<Lot>();

        public ICollection<Bid> Bids { get; set; } = new List<Bid>();
    }
}