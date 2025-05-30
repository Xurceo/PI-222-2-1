using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ShortDTOs
{
    public class CategoryShortDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }
        public IEnumerable<LotShortDTO> Lots { get; set; }
    }
}