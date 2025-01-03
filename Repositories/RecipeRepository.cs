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

        public async Task<PaginatedList<Recipe>> GetPaginatedRecipesAsync(int pageNumber, int pageSize)
        {
            var data = _dataContext.RecipeMaster.AsQueryable();
            var count = await data.CountAsync();
            var items = await data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<Recipe>(items, count, pageNumber, pageSize);
        }

        public async Task<List<Recipe>> GetFilteredAndSortedRecipesAsync(string? title, string? tags, string? sortBy, bool isDescending)
        {
            var query = _dataContext.RecipeMaster.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(r => r.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(tags))
            {
                query = query.Where(r => r.Tags != null && r.Tags.Contains(tags));
            }

            // Sorting
            query = sortBy?.ToLower() switch
            {
                "title" => isDescending ? query.OrderByDescending(r => r.Title) : query.OrderBy(r => r.Title),
                "tags" => isDescending ? query.OrderByDescending(r => r.Tags) : query.OrderBy(r => r.Tags),
                _ => query 
            };

            return await query.ToListAsync();
        }


    }
}
