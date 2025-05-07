using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Category? Parent { get; set; }

        public ICollection<Category> Subcategories { get; set; } = new List<Category>();

        public ICollection<Lot> Lots { get; set; } = new List<Lot>();
    }
}
