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
        public async Task<IEnumerable<LotDTO>> GetCategoryLots(Guid categoryId)
        {
            var category = await _unitOfWork.Categories.GetById(categoryId);
            if (category == null)
            {
                throw new ArgumentException("Category not found", nameof(categoryId));
            }
            return _mapper.Map<IEnumerable<LotDTO>>(category.Lots);
        }
        public async Task<IEnumerable<CategoryDTO>> GetCategorySubcategories(Guid parentId)
        {
            var parentCategory = await _unitOfWork.Categories.GetById(parentId);
            if (parentCategory == null)
            {
                throw new ArgumentException("Parent category not found", nameof(parentId));
            }
            return _mapper.Map<IEnumerable<CategoryDTO>>(parentCategory.Subcategories);
        }
        public async Task UpdateCategory(CategoryDTO dto)
        {
            var category = await _unitOfWork.Categories.GetById(dto.Id);

            ArgumentNullException.ThrowIfNull(category, "Category not found");

            _mapper.Map(dto, category);

            if (dto.LotIds != null)
            {
                // Get existing bids to remove (those not in the new list)
                var lotsToRemove = category.Lots
                    .Where(l => !dto.LotIds.Contains(l.Id))
                    .ToList();

                // Get bids to add (those not already in the collection)
                var existingLotIds = category.Lots.Select(l => l.Id).ToList();
                var lotsToAddIds = dto.LotIds.Except(existingLotIds);

                // Remove bids
                foreach (var lot in lotsToRemove)
                {
                    category.Lots.Remove(lot);
                }

                // Add new bids
                foreach (var lotId in lotsToAddIds)
                {
                    var lot = await _unitOfWork.Lots.GetById(lotId);
                    if (lot != null)
                    {
                        category.Lots.Add(lot);
                    }
                }
            }

            if (dto.SubcategoryIds != null)
            {
                var subcategoriesToRemove = category.Subcategories
                    .Where(sc => !dto.SubcategoryIds.Contains(sc.Id))
                    .ToList();
                var existingSubcategoryIds = category.Subcategories.Select(sc => sc.Id).ToList();
                var subcategoriesToAddIds = dto.SubcategoryIds.Except(existingSubcategoryIds);
                // Remove subcategories
                foreach (var subcategory in subcategoriesToRemove)
                {
                    category.Subcategories.Remove(subcategory);
                }
                // Add new subcategories
                foreach (var subcategory in existingSubcategoryIds)
                {
                    var subcategoryEntity = await _unitOfWork.Categories.GetById(subcategory);
                    if (subcategoryEntity != null)
                    {
                        category.Subcategories.Add(subcategoryEntity);
                    }
                }
            }

            await _unitOfWork.Categories.Update(category);
            await _unitOfWork.Categories.Save();
        }
    }
}