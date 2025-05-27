using BLL.CreateDTOs;
using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAll();
        CategoryDTO? GetById(Guid id);
        Guid AddCategory(CreateCategoryDTO category);
        void UpdateCategory(CategoryDTO category);
        void DeleteCategory(Guid id);
    }
}
