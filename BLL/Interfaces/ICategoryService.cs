using BLL.Models;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAll();
        CategoryDTO? GetById(int id);
        int AddCategory(CategoryDTO category);
        void UpdateCategory(CategoryDTO category);
        void DeleteCategory(int id);
    }
}
