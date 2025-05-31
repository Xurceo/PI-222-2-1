using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CreateDTOs
{
    public class PlaceBidDTO
    {
        public Guid LotId { get; set; }
        public decimal Amount { get; set; }
    }
}
