using AutoMapper;
using Ecommerce.Entities;
using Ecommerce.Models.DTO;
using Ecommerce.Repositories;

namespace Ecommerce.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IProductRepository productRepository, IMapper mapper, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public int CreateProduct(Product model)
        {
            try
            {
                //validate if product number already exists
                var existingProduct = _productRepository.GetProducts().FirstOrDefault(p => p.ProductNumber.Equals(model.ProductNumber, StringComparison.OrdinalIgnoreCase));
                if (existingProduct != null)
                {
                    _logger.LogError($"{existingProduct.ProductNumber} already exists.");
                    return 0;
                }

                // map model to new product object
                var product = _mapper.Map<ProductEntity>(model);
                _productRepository.CreateProduct(product);
                return product.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateProduct failed: {ex}");
            }
            return 0;
        }
        public IEnumerable<ProductEntity> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public ProductEntity GetProductById(int id)
        {
            return getProduct(id);
        }

        public void UpdateProduct(int id, Product model)
        {
            try
            {
                var product = getProduct(id);

                // copy model to prduct and save
                _mapper.Map(model, product);
                _productRepository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateProduct failed: {ex}");
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                var product = getProduct(id);
                _productRepository.DeleteProduct(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteProduct failed: {ex}");
            }

        }

        private ProductEntity getProduct(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                _logger.LogError("Product not found");
            }
            return product;
        }
    }
}
