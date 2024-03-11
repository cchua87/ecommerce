using Ecommerce.Entities;

namespace Ecommerce.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<ProductEntity> GetProducts();
        ProductEntity GetProductById(int id);
        void CreateProduct(ProductEntity product);
        void UpdateProduct(ProductEntity product);
        void DeleteProduct(ProductEntity product);
    }
}
