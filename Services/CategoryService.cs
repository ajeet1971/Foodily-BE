using Foodily.Interface;
using Foodily.ViewModels;

namespace Foodily.Services
{
    public class CategoryService : ICategoryService
    {
        private IConfiguration _config;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository, IConfiguration config)
        {
            _categoryRepository = categoryRepository;
            _config = config;
        }
        public async Task<int> AddCategoryAsync(CategoryVM category)
        {
            try
            {
                Category category1 = new()
                {
                    CategoryName = category.CategoryName
                };
                var categoryData = await _categoryRepository.AddCategoryAsync(category1);
                return categoryData ? 1 : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            try
            {
                var data = await _categoryRepository.GetAllCategoryAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            try
            {
                var data = await _categoryRepository.GetCategoryByIdAsync(id);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
