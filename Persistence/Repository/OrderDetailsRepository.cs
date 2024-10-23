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
    public class OrderDetailsRepository : IRepository<OrderDetails>
    {
        private readonly AppDbContext _context;

        public OrderDetailsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDetails>> GetAllAsync()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetails> GetByIdAsync(int id)
        {
            return await _context.OrderDetails.FindAsync(id);
        }

        public async Task<OrderDetails> AddAsync(OrderDetails entity)
        {
            await _context.OrderDetails.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(OrderDetails entity)
        {
            _context.OrderDetails.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.OrderDetails.FindAsync(id);
            if (category != null)
            {
                _context.OrderDetails.Remove(category);
               return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public IQueryable<OrderDetails> GetAllQueryable()
        {
            return _context.OrderDetails.Include(od =>od.Order).Include(od => od.Product).ThenInclude(p => p.Category).AsQueryable();
        }

        public int GetNextId()
        {
           throw new NotImplementedException();
        }
    }

}
