using System.Collections.Generic;

namespace BLL.Interfaces
{
    interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAllCategories();
        CategoryDTO GetCategoryById(int id);
        void AddCategory(CategoryDTO categoryDTO);
    }
}
