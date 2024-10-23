using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            CategoryConfigureations(modelBuilder);

            // Product configuration
            ProductConfigurations(modelBuilder);

            // Order configuration
            OrderConfigurations(modelBuilder);

            OrderDetailsConfigurations(modelBuilder);

            modelBuilder.Entity<OrderDetails>().ToTable(t => t.HasCheckConstraint("ck_quantity", "quantity > 0"));
            modelBuilder.Entity<OrderDetails>().ToTable(t => t.HasCheckConstraint("ck_unitprice", "unitprice >= 0"));
            modelBuilder.Entity<OrderDetails>().ToTable(t => t.HasCheckConstraint("ck_discount", "discount >= 0 AND discount <= 1"));

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);
        }

        private static void OrderDetailsConfigurations(ModelBuilder modelBuilder)
        {
            // OrderDetail configuration
            modelBuilder.Entity<OrderDetails>().ToTable("orderdetails");
            modelBuilder.Entity<OrderDetails>()
                .HasKey(od => new { od.OrderId, od.ProductId });
            modelBuilder.Entity<OrderDetails>()
                 .ToTable("orderdetails")
                 .Property(c => c.OrderId)
                 .HasColumnName("orderid");

            modelBuilder.Entity<OrderDetails>()
                  .ToTable("orderdetails")
                  .Property(c => c.ProductId)
                  .HasColumnName("productid");

            modelBuilder.Entity<OrderDetails>()
             .ToTable("orderdetails")
             .Property(c => c.UnitPrice)
             .HasColumnName("unitprice");

            modelBuilder.Entity<OrderDetails>()
             .ToTable("orderdetails")
             .Property(c => c.Quantity)
             .HasColumnName("quantity");

            modelBuilder.Entity<OrderDetails>()
           .ToTable("orderdetails")
           .Property(c => c.Discount)
           .HasColumnName("discount");
        }

        private static void OrderConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);
            modelBuilder.Entity<Order>()
            .ToTable("orders")
            .Property(c => c.Id)
            .HasColumnName("orderid");

            modelBuilder.Entity<Order>()
           .ToTable("orders")
           .Property(c => c.CustomerId)
           .HasColumnName("customerid");

            modelBuilder.Entity<Order>()
           .ToTable("orders")
           .Property(c => c.EmployeeId)
           .HasColumnName("employeeid");

            modelBuilder.Entity<Order>()
            .ToTable("orders")
            .Property(c => c.OrderDate)
            .HasColumnName("orderdate");

            modelBuilder.Entity<Order>()
          .ToTable("orders")
          .Property(c => c.RequiredDate)
          .HasColumnName("requireddate");

            modelBuilder.Entity<Order>()
            .ToTable("orders")
            .Property(c => c.ShippedDate)
            .HasColumnName("shippeddate");

            modelBuilder.Entity<Order>()
            .ToTable("orders")
            .Property(c => c.Freight)
            .HasColumnName("freight");

            modelBuilder.Entity<Order>()
            .ToTable("orders")
            .Property(c => c.ShipName)
            .HasColumnName("shipname");

            modelBuilder.Entity<Order>()
            .ToTable("orders")
            .Property(c => c.ShipAddress)
            .HasColumnName("shipaddress");

            modelBuilder.Entity<Order>()
            .ToTable("orders")
            .Property(c => c.ShipCity)
            .HasColumnName("shipcity");

            modelBuilder.Entity<Order>()
             .ToTable("orders")
             .Property(c => c.ShipPostalCode)
             .HasColumnName("shippostalcode");

            modelBuilder.Entity<Order>()
            .ToTable("orders")
            .Property(c => c.ShipCountry)
            .HasColumnName("shipcountry");
        }

        private static void ProductConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .ToTable("products")
                .Property(c => c.Id)
                .HasColumnName("productid");

            modelBuilder.Entity<Product>()
                .ToTable("products")
                .Property(c => c.Name)
                .HasColumnName("productname")
                .IsRequired();

            modelBuilder.Entity<Product>()
               .ToTable("products")
               .Property(c => c.SupplierId)
               .HasColumnName("supplierid");

            modelBuilder.Entity<Product>()
               .ToTable("products")
               .Property(c => c.CategoryId)
               .HasColumnName("categoryid");

            modelBuilder.Entity<Product>()
               .ToTable("products")
               .Property(c => c.QuantityPerUnit)
               .HasColumnName("quantityperunit");

            modelBuilder.Entity<Product>()
                .ToTable("products")
                .Property(c => c.UnitPrice)
                .HasColumnName("unitprice");

            modelBuilder.Entity<Product>()
               .ToTable("products")
               .Property(c => c.UnitsInStock)
               .HasColumnName("unitsinstock");




            modelBuilder.Entity<Product>().ToTable(t => t.HasCheckConstraint("ck_product_unitprice", "unitprice >= 0"));
            ;

            modelBuilder.Entity<Product>()
                .ToTable(p => p.HasCheckConstraint("ck_unitsinstock", "unitsinstock >= 0"));

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
        }

        private static void CategoryConfigureations(ModelBuilder modelBuilder)
        {
            // Category configuration
            modelBuilder.Entity<Category>().ToTable("categories");

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Category>()
                .ToTable("categories")
                .Property(c => c.Id)
                .HasColumnName("categoryid");

            modelBuilder.Entity<Category>()
                  .ToTable("categories")
                .Property(c => c.Name)
                .HasColumnName("categoryname")
                .IsRequired();

            modelBuilder.Entity<Category>()
             .ToTable("categories")
           .Property(c => c.Description)
           .HasColumnName("description");
        }
    }
}
