using Business.Models.Categories;
using Business.Models.Products;
using Business.Services.Categories;
using Business.Services.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
         private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }
        
        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await productsService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

         // GET: api/products/category/5
        [HttpGet("category/{id}")]
        public async Task<ActionResult<List<ProductDto>>> GetProductByCategory(int id)
        {
            var products = await productsService.GetByCategoryIdAsync(id);

            if (products == null || products.Count == 0)
            {
                return NotFound(products);
            }

            return Ok(products);
        }

          // GET: api/products/5
        [HttpGet]
        public async Task<ActionResult<List<ProductCategoryRelationDto>>> GetProduct([FromQuery]string name)
        {
            var products = await productsService.GetProductByName(name);

            if (products == null || products.Count == 0)
            {
                return NotFound(products);
            }

            return Ok(products);
        }
    }
}
