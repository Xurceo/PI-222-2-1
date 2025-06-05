using BLL.DTOs;
using BLL.CreateDTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BLL.ShortDTOs;

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
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetById(Guid id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpGet("{id}/lots")]
        public async Task<ActionResult<IEnumerable<LotShortDTO>>> GetCategoryLots(Guid id)
        {
            var lots = await _categoryService.GetCategoryLots(id);
            return Ok(lots);
        }

        [HttpGet("{id}/subcategories")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategorySubcategories(Guid id)
        {
            var subcategories = await _categoryService.GetCategorySubcategories(id);
            return Ok(subcategories);
        }

        [Authorize(Roles = "MANAGER,ADMIN")]
        [HttpPost]
        public async Task<ActionResult<Guid>> AddCategory([FromBody] CreateCategoryDTO dto)
        {
            var id = await _categoryService.AddCategory(dto);
            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }


        [Authorize(Roles = "MANAGER,ADMIN")]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO dto)
        {
            await _categoryService.UpdateCategory(dto);
            return NoContent();
        }
        
        [Authorize(Roles = "MANAGER,ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _categoryService.DeleteCategory(id);
            return NoContent();
        }
    }
}
