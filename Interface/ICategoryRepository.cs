using Foodily.ViewModels;

namespace Foodily.Interface
{
    public interface ICategoryRepository
    {
        Task<bool> AddCategoryAsync(Category category);
        Task<List<Category>> GetAllCategoryAsync();
        Task<Category> GetCategoryByIdAsync(int id);
    }
}
