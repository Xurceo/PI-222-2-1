using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public required string Name { get; set; }

        [ForeignKey("ParentId")]
        public Guid? ParentId { get; set; }
        public Category? Parent { get; set; }

        [InverseProperty(nameof(Parent))]
        public ICollection<Category> Subcategories { get; set; } = new List<Category>();

        public ICollection<Lot> Lots { get; set; } = new List<Lot>();
    }
}
