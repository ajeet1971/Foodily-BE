using Foodily.Interface;
using Foodily.Services;
using Foodily.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Foodily.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var result = await _categoryService.GetAllCategoryAsync();
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetAllRecipesById(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest(new { message = "Invalid Category ID." });
                }
                var result = await _categoryService.GetCategoryByIdAsync(id);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> AddCategoryDetails([FromBody] CategoryVM category)
        {
            try
            {
                var res = await _categoryService.AddCategoryAsync(category);
                if (res == 1)
                {
                    return StatusCode(StatusCodes.Status201Created, new { message = "category Added Successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error while adding category data.: " + ex });
            }
        }

    }
}
