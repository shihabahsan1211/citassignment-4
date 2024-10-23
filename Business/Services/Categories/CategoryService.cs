using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Business.Models.Categories;
using Business.Mapper;

namespace Business.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Domain.Entities.Category> repository;

        public CategoryService(IRepository<Domain.Entities.Category> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Models.Categories.Category>> GetAllAsync()
        {
            var allCategory = await repository.GetAllAsync();

            return allCategory.Select(CategoryMapper.Map);

        }

        public async Task<Models.Categories.Category> GetByIdAsync(int id)
        {
            var category = await repository.GetByIdAsync(id);

            return CategoryMapper.Map(category);
        }

        public async Task<Models.Categories.Category> AddAsync(CategoryCreateRequest request)
        {
            var category = await repository.AddAsync(CategoryMapper.Map(request));
            return CategoryMapper.Map(category);
        }

        public async Task<bool> UpdateAsync(CategoryUpdateRequest entity)
        {
            return await repository.UpdateAsync(CategoryMapper.Map(entity));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await repository.DeleteAsync(id);
        }
    }
}
