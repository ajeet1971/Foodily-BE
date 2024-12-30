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
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet("GetRecipeById")]
        public async Task<IActionResult> GetAllRecipesById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { message = "Invalid Recipe ID." });
                }
                var result = await _recipeService.GetRecipeByIdAsync(id);
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost("CreateRecipe")]
        public async Task<IActionResult> AddRecipeDetails([FromForm] RecipeVM recipe )
        {
            try
            {
                if (recipe == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest,new { message = "Recipe data is required." });
                }
                if (string.IsNullOrWhiteSpace(recipe.Title))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Recipe title is required." });
                }

                if (string.IsNullOrWhiteSpace(recipe.Ingredients))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Recipe ingredients are required." });
                }

                if (string.IsNullOrWhiteSpace(recipe.Instruction))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Recipe instructions are required." });
                }
                if (string.IsNullOrWhiteSpace(recipe.Cooktime))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Invalid cook time. Please enter a valid time" });
                }
                if (recipe.Photo == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Please enter a photo." });
                }
                var res = await _recipeService.AddRecipeAsync(recipe);
                if (res == 1)
                {
                    return StatusCode(StatusCodes.Status201Created, new { message = "Recipe Added Successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Failed to add recipe." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error while adding recipe details.: " + ex });
            }
        }

        [HttpPatch("UpdateRecipe")]
        public async Task<IActionResult> UpdateRecipe(int rid, [FromForm] RecipeVM recipe)
        {
            try
            {
                if (rid <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Invalid recipe ID." });
                }

                if (recipe == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Recipe data is required." });
                }


                var result = await _recipeService.UpdateRecipeAsync(rid, recipe);
                if (result)
                {
                    return StatusCode(StatusCodes.Status201Created, new { message = "Recipe updated Successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error while update recipe details.: " + ex });
            }
        }

        [HttpDelete("DeleteRecipe")]
        public async Task<IActionResult> DeleteRecipe(int rid)
        {
            try
            {
             
                var result = await _recipeService.DeleteRecipeAsync(rid);
                if (!result)
                {
                    return NotFound(new { message = $"Recipe with ID {rid} not found." });
                }
                if (result)
                {
                    return StatusCode(StatusCodes.Status201Created, new { message = "Recipe deleted Successfully" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error while delete recipe details.: " + ex });
            }
        }

    }
}
