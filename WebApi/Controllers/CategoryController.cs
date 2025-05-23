using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Добавьте методы аналогично UserController, используя методы ICategoryService
        [HttpGet]
        public ActionResult<CategoryDTO> GetAll()
        {
            var categories = _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public ActionResult<CategoryDTO> GetById(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public ActionResult<CategoryDTO> Create([FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest();
            }
            var id = _categoryService.AddCategory(categoryDto);
            return CreatedAtAction(nameof(GetById), new { id }, categoryDto);
        }
    }
}

