using Business.Models.Categories;
using Business.Models.Orders;
using Business.Models.Products;
using Business.Services.Categories;
using Business.Services.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Persistence.Context;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class DataService
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly ProductRepository _productRepository;
        private readonly OrderRepository _orderRepository;
        private readonly OrderDetailsRepository _orderDetailsRepository;
        private readonly ProductsService productsService;
        private readonly CategoryService categoryService;
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
            productsService = new ProductsService(_productRepository);
            categoryService = new CategoryService(_categoryRepository);
        }

        /* Categories */

        public List<Category> GetCategories()
        {
            return categoryService.GetAllAsync().Result.ToList();
        }

        public Category GetCategory(int id)
        {
            return categoryService.GetByIdAsync(id).Result;
        }

        public Category CreateCategory(string name, string description)
        {
            var category = categoryService.AddAsync(new CategoryCreateRequest
            {
                Name = name,
                Description = description
            }).GetAwaiter().GetResult();
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
            var product = _productRepository.GetAllQueryable().Where(x=> x.Id == id).FirstOrDefault();
            if(product == null) return null;

            return MapToProduct(product);

        }

        public List<Product> GetProductByCategory(int categoryId)
        {
            var products = _productRepository.GetAllQueryable().Where(x=> x.CategoryId == categoryId).ToList();

            return products.Select(MapToProduct).ToList();
        }

        public List<ProductCategoryRelationDto> GetProductByName(string nameSubstring)
        {
            return productsService.GetProductByName(nameSubstring).GetAwaiter().GetResult();
        }

        /* Orders */

        public Order GetOrder(int id)
        {
            var order = _orderRepository.GetAllQueryable().FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return null;
            }
            return MapOrderToDto(order);
        }

        private static Order MapOrderToDto(Domain.Entities.Order? order)
        {
            return new Order
            {
                Id = order.Id,
                Date = order.OrderDate,
                Required = order.RequiredDate,
                ShipCity = order.ShipCity,
                ShipName = order.ShipName,
                ShippedDate = order.ShippedDate,
                OrderDetails = order?.OrderDetails?.Select(od => MapToOrderDetails(od)).ToList(),

            };
        }

        private static OrderDetails MapToOrderDetails(Domain.Entities.OrderDetails od)
        {
            return new OrderDetails
            {
                Discount = od.Discount,
                OrderId = od.OrderId,
                Quantity = od.Quantity,
                UnitPrice = od.UnitPrice,
                Product = MapToProduct(od.Product),
            };
        }

        public List<Order> GetOrders()
        {
            return _orderRepository.GetAllAsync().Result.Select(MapOrderToDto).ToList();
        }

        /* Order Details */

        public List<OrderDetails> GetOrderDetailsByOrderId(int orderId)
        {
            var details = _orderDetailsRepository.GetAllQueryable().Where(x => x.OrderId == orderId).ToList();

            return details.Select(MapOrderToDto).ToList();
        }

        public List<OrderDetails> GetOrderDetailsByProductId(int productId)
        {
            var details =  _orderDetailsRepository.GetAllQueryable().Where(x => x.ProductId == productId).OrderBy(x=>x.OrderId).ToList();
            return details.Select(MapOrderToDto).ToList();
        }

        private static Models.Orders.OrderDetails MapOrderToDto(Domain.Entities.OrderDetails? od)
        {
            if (od == null) return null;
            return
                new Models.Orders.OrderDetails
                {
                    Discount = od.Discount,
                    OrderId = od.OrderId,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice,
                    Order = MapOrder(od.Order),
                    Product = MapToProduct(od.Product)
                };



        }

        private static Product MapToProduct(Domain.Entities.Product od)
        {
            return new Product
            {
                Name = od.Name,
                Id = od.Id,
                QuantityPerUnit = od.QuantityPerUnit,
                UnitPrice = od.UnitPrice,
                UnitsInStock = od.UnitsInStock,
                CategoryName = od.Category.Name,
                Category = new Category
                {
                    Id = od.Category.Id,
                    Name = od.Category.Name,
                    Description = od.Category.Description,
                }

            };
        }

        private static Models.Orders.Order MapOrder(Domain.Entities.Order od)
        {
            return new Models.Orders.Order
            {
                Id = od.Id,
                Date = od.OrderDate,
                Required = od.RequiredDate,
                ShipCity = od.ShipCity,
                ShipName = od.ShipName,
                ShippedDate = od.ShippedDate,
                OrderDetails = od.OrderDetails?.Select(MapToOrderDetails).ToList()
            };
        }
    }
}
