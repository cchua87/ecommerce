using Ecommerce.Entities;
using Ecommerce.Models.DTO;

namespace Ecommerce.Services
{
    public interface IProductService
    {
        IEnumerable<ProductEntity> GetProducts();
        ProductEntity GetProductById(int id);
        int CreateProduct(Product model);
        void UpdateProduct(int id, Product model);
        void DeleteProduct(int id);
    }
}
