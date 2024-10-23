using Business.Models.Categories;
using Business.Models.Products;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapper
{
    public static class ProductMapper
    {
        public static Models.Products.ProductDto Map(Domain.Entities.Product? product)
        {
            if(product == null) return null;

            return new Models.Products.ProductDto
            {
               Id = product.Id,
               Name = product.Name,
               CategoryName = product.Category.Name,
               QuantityPerUnit = product.QuantityPerUnit,
               UnitPrice = product.UnitPrice,
               UnitsInStock = product.UnitsInStock
               
            };
        }

        public static ProductCategoryRelationDto NameMap(Domain.Entities.Product? product)
        {
            if(product == null) return null;

            return new ProductCategoryRelationDto
            {

               ProductName = product.Name,
               CategoryName = product.Category.Name,
               
            };
        }
    }
}
