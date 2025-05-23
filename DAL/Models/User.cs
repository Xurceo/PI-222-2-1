using System.ComponentModel.DataAnnotations;


namespace DAL.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "Registered"; 

        public ICollection<Lot> Lots { get; set; } = new List<Lot>();

        public ICollection<Bid> Bids { get; set; } = new List<Bid>();
    }
}