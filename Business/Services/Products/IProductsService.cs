using Business.Models.Products;

namespace Business.Services.Products
{
    public interface IProductsService
    {
        Task<List<ProductDto>> GetByCategoryIdAsync(int categoryId);
        Task<ProductDto> GetByIdAsync(int id);

        Task<List<ProductCategoryRelationDto>> GetProductByName(string nameSubstring);
    }
}