using BLL.CreateDTOs;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAll();
        Task<CategoryDTO?> GetById(Guid id);
        Task<IEnumerable<LotDTO>> GetCategoryLots(Guid categoryId);
        Task<IEnumerable<CategoryDTO>> GetCategorySubcategories(Guid parentId);
        Task<Guid> AddCategory(CreateCategoryDTO category);
        Task UpdateCategory(CategoryDTO category);
        Task DeleteCategory(Guid id);
    }
}