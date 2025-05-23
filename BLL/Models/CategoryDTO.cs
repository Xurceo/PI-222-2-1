namespace BLL.Models
{
    public class CategoryDTO
    {
        public string Name { get; set; } = string.Empty;
        public CategoryDTO? Parent { get; set; }
        public ICollection<CategoryDTO> Subcategories { get; set; } = [];
    }
}
