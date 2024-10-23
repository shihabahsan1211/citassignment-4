using Business.Mapper;
using Business.Models.Categories;
using Business.Models.Products;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Products
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<Domain.Entities.Product> repository;

        public ProductsService(IRepository<Domain.Entities.Product> repository)
        {
            this.repository = repository;
        }

        public async Task<Models.Products.ProductDto> GetByIdAsync(int id)
        {
            var product = await repository.GetAllQueryable().Where(x => x.Id == id).FirstOrDefaultAsync();

            return ProductMapper.Map(product);
        }

        public async Task<List<Models.Products.ProductDto>> GetByCategoryIdAsync(int categoryId)
        {
            var products = await repository.GetAllQueryable().Where(x => x.CategoryId == categoryId).ToListAsync();

            return products.Select(ProductMapper.Map).ToList();
        }

        public async Task<List<ProductCategoryRelationDto>> GetProductByName(string nameSubstring)
        {
            var products = await repository.GetAllQueryable().Where(x => x.Name.ToLower().Contains(nameSubstring.ToLower())).OrderBy(o=> o.Id).ToListAsync();

            return products.Select(ProductMapper.NameMap).ToList();
        }
    }
}
