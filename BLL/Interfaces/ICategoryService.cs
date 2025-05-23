using BLL.Models;

namespace BLL.Interfaces
{
    interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAllCategories();
        void AddCategory(CategoryDTO category);
        void UpdateCategory(CategoryDTO category);
        void DeleteCategory(int id);
    }
}
