using Foodily.ViewModels;

namespace Foodily.Interface
{
    public interface IRecipeService
    {
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<List<Recipe>> GetAllRecipesAsync();
        Task<int> AddRecipeAsync(RecipeVM recipe);
        Task<bool> UpdateRecipeAsync(int rid,RecipeVM recipe);
        Task<bool> DeleteRecipeAsync(int rid);
        Task<PaginatedList<Recipe>> GetPaginatedRecipesAsync(int pageNumber, int pageSize);
        Task<List<Recipe>> GetFilteredAndSortedRecipesAsync(string title, string tags, string sortBy, bool isDescending);
    }
}
