using Foodily.ViewModels;

namespace Foodily.Interface
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryByIdAsync(int id);
        Task<List<Category>> GetAllCategoryAsync();
        Task<int> AddCategoryAsync(CategoryVM category);
    }
}
