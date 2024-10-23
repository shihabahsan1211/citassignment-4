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
    public class OrderRepository : IRepository<Order>
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Order> AddAsync(Order entity)
        {
            entity.Id = GetNextId();
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(Order entity)
        {
            _context.Orders.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public IQueryable<Order> GetAllQueryable()
        {
             return _context.Orders.Include(o => o.OrderDetails).ThenInclude(p => p.Product).ThenInclude(p => p.Category).AsQueryable();
        }

        public int GetNextId()
        {
           return _context.Orders.Max(c => c.Id) + 1;
        }
    }

}
