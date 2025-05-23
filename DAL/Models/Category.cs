using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Category? Parent { get; set; }

        public ICollection<Category> Subcategories { get; set; } = new List<Category>();

        public ICollection<Lot> Lots { get; set; } = new List<Lot>();
    }
}
