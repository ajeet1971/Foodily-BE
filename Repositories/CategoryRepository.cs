using Foodily.Data;
using Foodily.Interface;
using Foodily.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Foodily.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly DataContext _dataContext;
        public CategoryRepository(DataContext dataContext) { _dataContext = dataContext; }


        public async Task<bool> AddCategoryAsync(Category category)
        {
            await _dataContext.AddAsync(category);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            var data = await _dataContext.CategoryMaster.ToListAsync();
            return data;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var data = await _dataContext.CategoryMaster.FindAsync(id);
            return data;
        }
    }
}
