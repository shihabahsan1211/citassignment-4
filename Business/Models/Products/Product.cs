using Business.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string QuantityPerUnit { get; set; }
        public string CategoryName { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

          public Category Category { get; set; }

    }
}
