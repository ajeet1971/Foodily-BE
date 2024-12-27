using Foodily.Data;
using Foodily.Interface;
using Foodily.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Foodily.Repositories
{
    public class RecipeRepository: IRecipeRepository
    {
        private readonly DataContext _dataContext;
        public RecipeRepository(DataContext dataContext) { _dataContext = dataContext; }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            var data = await _dataContext.RecipeMaster.FindAsync(id);
            return data;
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            var data = await _dataContext.RecipeMaster.ToListAsync();
            return data;
        }

        public async Task<bool> AddRecipeAsync(Recipe recipe)
        {
            await _dataContext.AddAsync(recipe);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRecipeAsync(Recipe recipe)
        {
            _dataContext.Update(recipe);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRecipeAsync(int rid)
        {
            var recipe = await _dataContext.RecipeMaster.FirstOrDefaultAsync(r=>r.Id ==rid);
            if (recipe != null)
            {
                _dataContext.RecipeMaster.Remove(recipe);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
