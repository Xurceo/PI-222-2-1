using BLL.CreateDTOs;
using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
        public async Task GetAll_ReturnsOkWithCategories()
        {
            var categories = new List<CategoryDTO> { new CategoryDTO() { Name = "Test" } };
            _mockCategoryService.Setup(x => x.GetAll()).ReturnsAsync(categories);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result); 
            Assert.Equal(categories, okResult.Value);
        }



        [Fact]
        public async Task AddCategory_WithValidData_ReturnsCreated()
        {
            var createDto = new CreateCategoryDTO { Name = "Test" };
            var categoryId = Guid.NewGuid();
            _mockCategoryService.Setup(x => x.AddCategory(createDto)).ReturnsAsync(categoryId);

            var result = await _controller.AddCategory(createDto);

            Assert.IsType<OkObjectResult>(result.Result);
        }

    }

}
