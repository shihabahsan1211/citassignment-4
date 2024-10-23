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
    public class ProductRepository : IRepository<Product>
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddAsync(Product entity)
        {
            entity.Id = GetNextId();
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(Product entity)
        {
            _context.Products.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

         public async Task<IList<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Products.Include(p => p.Category).Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        public IQueryable<Product> GetAllQueryable()
        {
            return _context.Products.Include(p => p.Category).Include( p => p.OrderDetails).AsQueryable();
        }

        public int GetNextId()
        {
           return _context.Products.Max(c => c.Id) + 1;
        }
    }

}
