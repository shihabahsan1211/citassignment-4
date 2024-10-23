using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> AddAsync(Category entity)
        {
            entity.Id = GetNextId();
            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(Category entity)
        {
              var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x=> x.Id == entity.Id);
            if (category != null)
            {
            _context.Categories.Update(entity);
            return await _context.SaveChangesAsync() > 0;
            }
            return false;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x=> x.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
               return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public IQueryable<Category> GetAllQueryable()
        {
             return _context.Categories.Include(c => c.Products).AsNoTracking().AsQueryable();
        }

        public int GetNextId()
        {
           return _context.Categories.Max(c => c.Id) + 1;
        }
    }

}
