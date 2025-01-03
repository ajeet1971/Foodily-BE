using Foodily.ViewModels;

namespace Foodily.Interface
{
    public interface IRecipeRepository
    {
        Task<bool> AddRecipeAsync(Recipe recipe);
        Task<List<Recipe>> GetAllRecipesAsync();
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<bool> UpdateRecipeAsync(Recipe recipe);
        Task<bool> DeleteRecipeAsync(int rid);
        Task<PaginatedList<Recipe>> GetPaginatedRecipesAsync(int pageNumber, int pageSize);
        Task<List<Recipe>> GetFilteredAndSortedRecipesAsync (string title, string tags, string sortBy, bool isDescending);
    }
}
