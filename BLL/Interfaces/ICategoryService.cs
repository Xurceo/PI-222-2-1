using BLL.Models;

namespace BLL.Interfaces
{
    interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAll();
        CategoryDTO? GetById(int id);
        void AddCategory(CategoryDTO category);
        void UpdateCategory(CategoryDTO category);
        void DeleteCategory(int id);
    }
}
