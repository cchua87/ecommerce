using Ecommerce.Entities;
using System.Diagnostics;

namespace Ecommerce.Data
{
    public class DbInitializer
    {
        private DataContext _context;
        public DbInitializer(DataContext context)
        {
            _context = context;
        }
        public void Run()
        {
            _context.Database.EnsureCreated();

            // Look for any products.
            if (_context.Products.Any())
            {
                return;   // DB has been seeded
            }

            var products = new ProductEntity[]
            {
                new ProductEntity{Name = "HL Road Frame - Black", ProductNumber = "FR-R92B-58", Price = 1431},
                new ProductEntity{Name = "Sport-100 Helmet", ProductNumber = "HL-U509-R", Price = 35},
                new ProductEntity{Name = "Mountain Bike Socks", ProductNumber = "SO-B909-L", Price = 10},
                new ProductEntity{Name = "ML Road Frame - Red", ProductNumber = "FR-R72R-44", Price = 590},
                new ProductEntity{Name = "HL Mountain Frame - Silver", ProductNumber = "FR-M94S-38", Price = 1364}
            };
            foreach (var product in products)
            {
                _context.Products.Add(product);
            }
            _context.SaveChanges();
        }
    }
}
