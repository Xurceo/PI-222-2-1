using BLL.CreateDTOs;
using BLL.Interfaces;
using BLL.ShortDTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace Tests
{
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoryService> _mockCategoryService;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _controller = new CategoryController(_mockCategoryService.Object);
        }

        [Fact]
        public async Task AddCategory_WithValidData_ReturnsCreated()
        {
            var createDto = new CreateCategoryDTO { Name = "Test" };
            var categoryId = Guid.NewGuid();
            _mockCategoryService.Setup(x => x.AddCategory(createDto)).ReturnsAsync(categoryId);

            var result = await _controller.AddCategory(createDto);

            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result); 
            Assert.Equal(nameof(CategoryController.GetById), createdAtResult.ActionName);
        }

    }

}
