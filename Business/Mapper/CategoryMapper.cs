using Business.Models.Categories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapper
{
    public static class CategoryMapper
    {
        public static Models.Categories.Category Map(Domain.Entities.Category category)
        {
            if(category == null) return null;

            return new Models.Categories.Category
            {
                Description = category.Description,
                Id = category.Id,
                Name = category.Name,
            };
        }

        public static Domain.Entities.Category Map(CategoryCreateRequest category)
        {
            return new Domain.Entities.Category
            {
                Description = category.Description,
                Name = category.Name,
            };
        }

        public static Domain.Entities.Category Map(CategoryUpdateRequest category)
        {
            return new Domain.Entities.Category
            {
                Description = category.Description,
                Name = category.Name,
                Id = category.Id,
            };
        }
    }
}
