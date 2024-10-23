using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataService
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly ProductRepository _productRepository;
        private readonly OrderRepository _orderRepository;
        private readonly OrderDetailsRepository _orderDetailsRepository;
        private readonly AppDbContext appDbContext;
        // Define your connection string
        string connectionString = "Host=localhost;Database=northwind;Username=db_user;Password=123456";

        public DataService()
        {
            // Set up the DbContextOptions with the connection string
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        //// Instantiate the DbContext
        appDbContext = new AppDbContext(options);
            
            _categoryRepository = new CategoryRepository(appDbContext);
            _productRepository = new ProductRepository(appDbContext);
            _orderRepository = new OrderRepository(appDbContext);
            _orderDetailsRepository = new OrderDetailsRepository(appDbContext);
        }

        /* Categories */

        public List<Category> GetCategories()
        {
            return _categoryRepository.GetAllAsync().Result.ToList();
        }

        public Category GetCategory(int id)
        {
            return _categoryRepository.GetByIdAsync(id).Result;
        }

        public Category CreateCategory(string name, string description)
        {
            var category = new Category { Name = name, Description = description };
            _categoryRepository.AddAsync(category).Wait();
            return category;
        }

        public bool DeleteCategory(int id)
        {
            var category = _categoryRepository.GetByIdAsync(id).Result;
            if (category == null) return false;

            _categoryRepository.DeleteAsync(category.Id).GetAwaiter().GetResult(); // Implement DeleteCategory in repository
            return true;
        }

        public bool UpdateCategory(int id, string name, string description)
        {
            var category = _categoryRepository.GetByIdAsync(id).Result;
            if (category == null) return false;

            category.Name = name;
            category.Description = description;
            _categoryRepository.UpdateAsync(category).Wait();
            return true;
        }

        /* Products */

        public Product GetProduct(int id)
        {
            return _productRepository.GetAllQueryable().FirstOrDefault(x => x.Id == id);
        }

        public List<Product> GetProductByCategory(int categoryId)
        {
            return _productRepository.GetAllQueryable().Where(x => x.CategoryId == categoryId).ToList();
        }

        public List<Product> GetProductByName(string nameSubstring)
        {
            return _productRepository.GetAllQueryable().Where(x => x.Name.Contains(nameSubstring)).ToList();
        }

        /* Orders */

        public Order GetOrder(int id)
        {
            return _orderRepository.GetAllQueryable().FirstOrDefault(x=>x.Id == id);
        }

        public List<Order> GetOrders()
        {
            return _orderRepository.GetAllAsync().Result.ToList();
        }

        /* Order Details */

        public List<OrderDetails> GetOrderDetailsByOrderId(int orderId)
        {
            return _orderDetailsRepository.GetAllQueryable().Where(x => x.OrderId == orderId).ToList();
        }

        public List<OrderDetails> GetOrderDetailsByProductId(int productId)
        {
            return _orderDetailsRepository.GetAllQueryable().Where(x => x.ProductId == productId).ToList();
        }
    }
}
