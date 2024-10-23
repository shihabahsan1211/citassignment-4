using Business.Models.Categories;

namespace Business.Services.Categories
{
    public interface ICategoryService
    {
        Task<Category> AddAsync(CategoryCreateRequest request);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<bool> UpdateAsync(CategoryUpdateRequest entity);
    }
}