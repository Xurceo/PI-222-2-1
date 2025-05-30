using BLL.CreateDTOs;
using BLL.DTOs;
using BLL.ShortDTOs;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAll();
        Task<IEnumerable<CategoryShortDTO>> GetAllShort();
        Task<CategoryDTO?> GetById(Guid id);
        Task<Guid> AddCategory(CreateCategoryDTO category);
        Task UpdateCategory(CategoryDTO category);
        Task DeleteCategory(Guid id);
    }
}