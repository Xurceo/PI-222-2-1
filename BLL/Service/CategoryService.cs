using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Models;
using DAL.UoW;

namespace BLL.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public int AddCategory(CategoryDTO dto)
        {
            var category = _mapper.Map<Category>(dto);

            _unitOfWork.Categories.Create(category);
            _unitOfWork.Save();

            return category.Id;
        }
        public void DeleteCategory(int id)
        {
            _unitOfWork.Categories.Delete(id);
            _unitOfWork.Save();
        }
        public IEnumerable<CategoryDTO> GetAll()
        {
            var categories = _unitOfWork.Categories.GetAll();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }
        public CategoryDTO? GetById(int id)
        {
            var category = _unitOfWork.Categories.GetById(id);
            return category != null ? _mapper.Map<CategoryDTO>(category) : null;
        }
        public void UpdateCategory(CategoryDTO dto)
        {
            var category = _mapper.Map<Category>(dto);
            _unitOfWork.Categories.Update(category);
            _unitOfWork.Save();
        }
    }
}
