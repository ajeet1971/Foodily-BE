using Foodily.Interface;
using Foodily.ViewModels;

namespace Foodily.Services
{
    public class RecipeService : IRecipeService
    {
        private IConfiguration _config;
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository, IConfiguration config)
        {
            _recipeRepository = recipeRepository;
            _config = config;
        }
        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            try
            {
                var data = await _recipeRepository.GetAllRecipesAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }                                                                                                                                       
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            try
            {
                var data = await _recipeRepository.GetRecipeByIdAsync(id);
                return data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> AddRecipeAsync(RecipeVM recipe)
        {
            try
            {
                string imagePath = string.Empty;

                if (!string.IsNullOrEmpty(recipe.Photo))
                {
                    var fileName = recipe.Photo;

                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwRoot", "Images");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var filePath = Path.Combine(directoryPath, fileName);

                    Console.WriteLine($"Saving file to: {filePath}");
                    imagePath = "/Images/" + fileName;

                    // Log the image path
                    Console.WriteLine($"Image path: {imagePath}");
                }
                Recipe recipe1 = new()
                {
                    Title = recipe.Title,
                    Photo = imagePath,
                    Ingredients = recipe.Ingredients,
                    Instruction = recipe.Instruction,
                    Tags = recipe.Tags,
                    Description = recipe.Description,
                    Cooktime = recipe.Cooktime,
                    Preptime = recipe.Preptime,
                    Difficulty = recipe.Difficulty,
                };
                var recipeData = await _recipeRepository.AddRecipeAsync(recipe1);
                return recipeData ? 1 : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateRecipeAsync(int rid, RecipeVM recipe)
        {
            try
            {
                if(recipe == null)
                {
                    return false;
                }
                if (rid == 0)
                {
                    return false;
                }
                var recipeDetail = await _recipeRepository.GetRecipeByIdAsync(rid);
                if (recipeDetail == null)
                {
                    return false;
                }
                string imagePath = recipeDetail.Photo; 

                if (!string.IsNullOrEmpty(recipe.Photo))  
                {
                    var fileName = recipe.Photo;
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwRoot", "Images");

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var filePath = Path.Combine(directoryPath, fileName);

                    Console.WriteLine($"Saving file to: {filePath}");
                    imagePath = "/Images/" + fileName;
                }
                recipeDetail.Title = recipe.Title;
                recipeDetail.Photo = imagePath;
                recipeDetail.Ingredients = recipe.Ingredients;
                recipeDetail.Tags = recipe.Tags;
                recipeDetail.Description = recipe.Description;
                recipeDetail.Cooktime = recipe.Cooktime;
                recipeDetail.Preptime = recipe.Preptime;
                recipeDetail.Difficulty = recipe.Difficulty;
                var recipeData = await _recipeRepository.UpdateRecipeAsync(recipeDetail);
                return recipeData;
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                throw ex; 
            }
        }

        public async Task<bool> DeleteRecipeAsync(int rid)
        {
            try
            {
                if (rid == 0)
                {
                    return false;
                }
                var recipeData = await _recipeRepository.DeleteRecipeAsync(rid);
                return recipeData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
