using Foodily.Interface;
using Foodily.Services;
using Foodily.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Foodily.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("GetAllRecipes")]
        public async Task<IActionResult> GetAllRecipes()
        {
            try
            {
                var result = await _recipeService.GetAllRecipesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRecipeById")]
        public async Task<IActionResult> GetAllRecipesById(int id)
        {
            try
            {
                var result = await _recipeService.GetRecipeByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateRecipe")]
        public async Task<IActionResult> AddRecipeDetails([FromBody] RecipeVM recipe)
        {
            try
            {
                var res = await _recipeService.AddRecipeAsync(recipe);
                if (res == 1)
                {
                    return StatusCode(StatusCodes.Status201Created, "Recipe Added Successfully");
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding recipe details.: " + ex);
            }
        }

        [HttpPatch("UpdateRecipe")]
        public async Task<IActionResult> UpdateRecipe(int rid, [FromBody] RecipeVM recipe)
        {
            try
            {
                var result = await _recipeService.UpdateRecipeAsync(rid, recipe);
                if (result)
                {
                    return StatusCode(StatusCodes.Status201Created, "Recipe updated Successfully");
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while update recipe details.: " + ex);
            }
        }

        [HttpDelete("DeleteRecipe")]
        public async Task<IActionResult> DeleteRecipe(int rid)
        {
            try
            {
                var result = await _recipeService.DeleteRecipeAsync(rid);
                if (result)
                {
                    return StatusCode(StatusCodes.Status201Created, "Recipe deleted Successfully");
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while delete recipe details.: " + ex);
            }
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok("Test");
        //}
    }
}
