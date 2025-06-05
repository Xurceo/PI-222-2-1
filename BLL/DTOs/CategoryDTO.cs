namespace BLL.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }
        public IEnumerable<Guid> SubcategoryIds { get; set; } = [];
        public IEnumerable<Guid> LotIds { get; set; } = [];
    }
}
