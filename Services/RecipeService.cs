using Foodily.Interface;
using Foodily.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
                if (id <= 0)
                {
                    throw new ArgumentException("Invalid user ID.");
                }
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
                if (string.IsNullOrWhiteSpace(recipe.Title))
                {
                    throw new ArgumentException("Recipe title is required.");
                }

                if (string.IsNullOrWhiteSpace(recipe.Ingredients))
                {
                    throw new ArgumentException("Recipe ingredients are required.");
                }

                if (string.IsNullOrWhiteSpace(recipe.Instruction))
                {
                    throw new ArgumentException("Recipe instructions are required.");
                }

                if (string.IsNullOrWhiteSpace(recipe.Cooktime))
                {
                    throw new ArgumentException("Invalid cook time.");
                }
                var ImageName= string.Empty;
                if (recipe.Photo != null && recipe.Photo.Length > 0)
                {
                    //string imagePath = string.Empty;
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(recipe.Photo.FileName);
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    var filePath = Path.Combine(directoryPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await recipe.Photo.CopyToAsync(stream);
                    }
                    ImageName = "/Images/" + fileName;
                }
                Recipe recipe1 = new()
                {
                    Title = recipe.Title,
                    Photo = ImageName,
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

                if (recipeDetail.Photo != null && recipeDetail.Photo.Length > 0)
                {
                    string imagePath = string.Empty;
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(recipe.Photo.FileName);
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    var filePath = Path.Combine(directoryPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await recipe.Photo.CopyToAsync(stream);
                    }
                    imagePath = "/Images/" + fileName;
                }
                recipeDetail.Title = recipe.Title;
                recipeDetail.Photo = recipe.Photo.FileName;
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
                if (recipeData == null)
                {
                    return false;
                }
                return recipeData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PaginatedList<Recipe>> GetPaginatedRecipesAsync(int pageNumber, int pageSize)
        {
            try
            {
                var data = await _recipeRepository.GetPaginatedRecipesAsync(pageNumber, pageSize);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Recipe>> GetFilteredAndSortedRecipesAsync(string? title, string? tags, string? sortBy, bool isDescending)
        {
            try
            {
                return await _recipeRepository.GetFilteredAndSortedRecipesAsync(title, tags, sortBy, isDescending);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
