using System.Text.Json.Serialization;

namespace BLL.CreateDTOs
{
    public class CreateLotDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal StartPrice { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [JsonIgnore]
        public Guid? OwnerId { get; set; }
    }
}
