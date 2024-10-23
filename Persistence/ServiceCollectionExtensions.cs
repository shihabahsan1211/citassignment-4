using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class ServiceCollectionExtensions
{
   public static void AddPersistanceServices(this IServiceCollection services)
    {
      services.AddScoped<IRepository<Category>, CategoryRepository>();
      services.AddScoped<IRepository<Product>, ProductRepository>();
      services.AddScoped<IRepository<Order>, OrderRepository>();
      services.AddScoped<IRepository<OrderDetails>, OrderDetailsRepository>();
    }
}
}
