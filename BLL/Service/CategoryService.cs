using AutoMapper;
using BLL.Interfaces;
using BLL.DTOs;
using DAL.Models;
using DAL.UoW;
using BLL.CreateDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Guid> AddCategory(CreateCategoryDTO category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category data cannot be null");
            }
            if (category.ParentId.HasValue && !await _unitOfWork.Categories.Exists(category.ParentId.Value))
            {
                throw new ArgumentException($"Parent category with id {category.ParentId} does not exist", nameof(category));
            }

            var mappedCategory = _mapper.Map<Category>(category);

            await _unitOfWork.Categories.Create(mappedCategory);
            await _unitOfWork.Categories.Save();
            return mappedCategory.Id;
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

        /// <summary>
        /// Обновляет категорию: валидация, маппинг скалярных полей, синхронизация коллекций.
        /// Основная логика разбита на приватные методы для лучшей читаемости.
        /// </summary>
        public async Task UpdateCategory(CategoryDTO category)
        {
            ArgumentNullException.ThrowIfNull(category);

            var dbCategory = await _unitOfWork.Categories.GetById(category.Id) ?? throw new ArgumentException("Category not found", nameof(category));

            await ValidateParentAsync(category);

            // Map scalar properties only — keep navigation collections in sync manually
            _mapper.Map(category, dbCategory);

            await SyncLotsAsync(category, dbCategory);
            await SyncSubcategoriesAsync(category, dbCategory);

            await _unitOfWork.Categories.Update(dbCategory);
            await _unitOfWork.Categories.Save();
        }

        // Проверяет ParentId на корректность (самостоятельность и существование)
        private async Task ValidateParentAsync(CategoryDTO category)
        {
            if (category.ParentId.HasValue)
            {
                if (category.ParentId.Value == category.Id)
                    throw new ArgumentException("Category cannot be parent of itself", nameof(category));

                if (!await _unitOfWork.Categories.Exists(category.ParentId.Value))
                    throw new ArgumentException($"Parent category with id {category.ParentId} does not exist", nameof(category));
            }
        }

        // Синхронизация коллекции Lots в dbCategory с id'ами из DTO
        private async Task SyncLotsAsync(CategoryDTO category, Category dbCategory)
        {
            if (category.LotIds == null)
                return;

            dbCategory.Lots ??= new List<Lot>();

            var existingLots = dbCategory.Lots;
            var existingLotIds = existingLots.Select(l => l.Id).ToHashSet();

            // Удаляем лоты, которых нет в DTO
            var lotsToRemove = existingLots.Where(l => !category.LotIds.Contains(l.Id)).ToList();
            foreach (var lot in lotsToRemove)
                existingLots.Remove(lot);

            // Добавляем отсутствующие лоты
            var lotsToAddIds = category.LotIds.Where(id => !existingLotIds.Contains(id));
            foreach (var lotId in lotsToAddIds)
            {
                var lot = await _unitOfWork.Lots.GetById(lotId);
                if (lot != null)
                    existingLots.Add(lot);
            }
        }

        // Синхронизация коллекции Subcategories в dbCategory с id'ами из DTO
        private async Task SyncSubcategoriesAsync(CategoryDTO category, Category dbCategory)
        {
            if (category.SubcategoryIds == null)
                return;

            dbCategory.Subcategories ??= new List<Category>();

            var existingSubs = dbCategory.Subcategories;
            var existingSubIds = existingSubs.Select(sc => sc.Id).ToHashSet();

            // Удаляем субкатегории, которых нет в DTO
            var subsToRemove = existingSubs.Where(sc => !category.SubcategoryIds.Contains(sc.Id)).ToList();
            foreach (var sc in subsToRemove)
                existingSubs.Remove(sc);

            // Добавляем отсутствующие субкатегории (избегаем добавления самой категории)
            var subsToAddIds = category.SubcategoryIds.Where(id => id != dbCategory.Id && !existingSubIds.Contains(id));
            foreach (var subId in subsToAddIds)
            {
                var subEntity = await _unitOfWork.Categories.GetById(subId);
                if (subEntity != null)
                    existingSubs.Add(subEntity);
            }
        }
    }
}