using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ShortDTOs
{
    public class LotShortDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal StartPrice { get; set; }
        public int Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public required UserShortDTO Owner { get; set; }
        public UserShortDTO? Winner { get; set; }
        public required CategoryShortDTO Category { get; set; }
        public IEnumerable<BidShortDTO> Bids { get; set; } = [];
    }

}
