using AutoMapper;
using BLL.Interfaces;
using BLL.DTOs;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using BLL.CreateDTOs;

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

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> GetAll()
        {
            var categories = _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryDTO> GetById(Guid id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public ActionResult<CategoryDTO> AddCategory([FromBody] CreateCategoryDTO dto)
        {
            var id = _categoryService.AddCategory(dto);

            return CreatedAtAction(nameof(AddCategory), new { id }, dto);
        }

        [HttpPost("update")]
        public ActionResult<CategoryDTO> UpdateCategory([FromBody] CategoryDTO dto)
        {
            _categoryService.UpdateCategory(dto);
            return NoContent();
        }
    }
}

