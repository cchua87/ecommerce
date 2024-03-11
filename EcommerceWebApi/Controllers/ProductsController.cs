using Ecommerce.Entities;
using Ecommerce.Models.DTO;
using Ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductService productsService, ILogger<ProductsController> logger)
        {
            _productService = productsService;
            _logger = logger;
        }

        // GET: Products
        [HttpGet]
        public IActionResult Index()
        {
            var products = _productService.GetProducts();
            return Ok(products);
        }

        // POST: Products
        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            var id = _productService.CreateProduct(product);
            _logger.LogInformation($"{id} product created.");
            return Created($"/Products/{id}", id);
        }

        // GET: Products/id
        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            var products = _productService.GetProductById(id);
            return Ok(products);
        }

        //PUT: Products/id
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            _productService.UpdateProduct(id, product);
            _logger.LogInformation($"Product {id} is updated.");
            return Ok(new { message = $"Product {id} is updated." });
        }

        //DELETE: Products/id
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            _logger.LogInformation($"Product {id} is deleted.");
            return Ok(new { message = $"Product {id} is deleted." });
        }

        // GET: Products/featured
        [HttpGet("featured")]
        public IActionResult GetFeaturedProducts()
        {
            var products = _productService.GetProducts().Take(3);
            return Ok(products);
        }
    }
}