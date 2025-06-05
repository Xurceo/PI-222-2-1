using AutoMapper;
using BLL.Interfaces;
using BLL.DTOs;
using DAL.Models;
using DAL.UoW;
using BLL.CreateDTOs;

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
        public async Task<Guid> AddCategory(CreateCategoryDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Category data cannot be null");
            }
            if (dto.ParentId.HasValue && !await _unitOfWork.Categories.Exists(dto.ParentId.Value))
            {
                throw new ArgumentException($"Parent category with id {dto.ParentId} does not exist", nameof(dto.ParentId));
            }

            var category = _mapper.Map<Category>(dto);

            await _unitOfWork.Categories.Create(category);
            await _unitOfWork.Categories.Save();
            return category.Id;
        }
        public async Task DeleteCategory(Guid id)
        {
            _unitOfWork.Categories.Delete(id);
            await _unitOfWork.Categories.Save();
        }
        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAll(c => c.Lots, c => c.Subcategories);
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }
        public async Task<CategoryDTO?> GetById(Guid id)
        {
            var category = await _unitOfWork.Categories.GetById(id);
            return category != null ? _mapper.Map<CategoryDTO>(category) : null;
        }
        public async Task UpdateCategory(CategoryDTO dto)
        {
            var category = await _unitOfWork.Categories.GetById(dto.Id);

            _mapper.Map(dto, category);
            await _unitOfWork.Categories.Update(category);
            await _unitOfWork.Categories.Save();
        }
    }
}