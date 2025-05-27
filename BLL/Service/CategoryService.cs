using AutoMapper;
using BLL.Interfaces;
using BLL.DTOs;
using DAL.Models;
using DAL.UoW;
using Microsoft.EntityFrameworkCore;
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
        public Guid AddCategory(CreateCategoryDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "Category data cannot be null");
            }
            if (dto.ParentId.HasValue && !_unitOfWork.Categories.Exists(dto.ParentId.Value))
            {
                throw new ArgumentException($"Parent category with id {dto.ParentId} does not exist", nameof(dto.ParentId));
            }

            var category = _mapper.Map<Category>(dto);

            _unitOfWork.Categories.Create(category);
            _unitOfWork.Save();
            return category.Id;
        }
        public void DeleteCategory(Guid id)
        {
            _unitOfWork.Categories.Delete(id);
            _unitOfWork.Save();
        }
        public IEnumerable<CategoryDTO> GetAll()
        {
            var categories = _unitOfWork.Categories.GetAll(c => c.Lots);
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }
        public CategoryDTO? GetById(Guid id)
        {
            var category = _unitOfWork.Categories.GetById(id);
            return category != null ? _mapper.Map<CategoryDTO>(category) : null;
        }
        public void UpdateCategory(CategoryDTO dto)
        {
            var category = _unitOfWork.Categories.GetById(dto.Id);

            _mapper.Map(dto, category);
            _unitOfWork.Categories.Update(category);
            _unitOfWork.Save();
        }
    }
}
