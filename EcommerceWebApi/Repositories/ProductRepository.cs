using Ecommerce.Data;
using Ecommerce.Entities;
using Ecommerce.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private DataContext _context;
        public ProductRepository(DataContext context)
        {
                _context = context;
        }
        public void CreateProduct(ProductEntity product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(ProductEntity product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public ProductEntity GetProductById(int id)
        {
            return _context.Products.Find(id);
        }

        public IEnumerable<ProductEntity> GetProducts()
        {
            return _context.Products;
        }

        public void UpdateProduct(ProductEntity product)
        {
            _context.Products.Update(product);
            _context.SaveChanges(true);
        }
    }
}
