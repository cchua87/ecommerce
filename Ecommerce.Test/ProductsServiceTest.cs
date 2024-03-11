using AutoMapper;
using Ecommerce.Entities;
using Ecommerce.Models.DTO;
using Ecommerce.Repositories;
using Ecommerce.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Ecommerce.Test
{
    public class ProductsServiceTest
    {
        private Mock<IProductRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<ProductService>> _mockLogger;
        private IEnumerable<ProductEntity> _products;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IProductRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<ProductService>>();
            _products = MockProducts();
        }

        [Test]
        public void CreateProduct_WhenProductNumberAlreadyExists_ThenReturnException()
        {
            // Arrange
            var productService = new ProductService(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
            var model = new Product { ProductNumber = "123", Name = "Test Product", Price = 10 };
            _mockRepository.Setup(x => x.GetProducts()).Returns(_products);

            // Act
            var result = productService.CreateProduct(model);

            // Assert
            Assert.That(result, Is.EqualTo(0));
            _mockRepository.Verify(repo => repo.GetProducts(), Times.Once);
            _mockRepository.Verify(repo => repo.CreateProduct(It.IsAny<ProductEntity>()), Times.Never);
        }

        [Test]
        public void CreateProduct_WhenProductNumberDoesNotExists_ThenReturnProductId()
        {
            // Arrange
            var productService = new ProductService(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
            var model = new Product { ProductNumber = "444", Name = "Product D", Price = 14 };
            _mockRepository.Setup(x => x.GetProducts()).Returns(_products);
            _mockMapper.Setup(mapper => mapper.Map<ProductEntity>(model)).Returns(new ProductEntity { Id = 5 });
            // Act
            var result = productService.CreateProduct(model);

            // Assert
            Assert.That(result, Is.EqualTo(5));
            _mockRepository.Verify(repo => repo.GetProducts(), Times.Once);
            _mockRepository.Verify(repo => repo.CreateProduct(It.IsAny<ProductEntity>()), Times.Once);
        }

        [Test]
        public void GetProducts_ThenReturnProducts()
        {
            // Arrange
            var productService = new ProductService(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
            _mockRepository.Setup(x => x.GetProducts()).Returns(_products);

            // Act
            var result = productService.GetProducts();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(4));
            _mockRepository.Verify(repo => repo.GetProducts(), Times.Once);
        }

        [Test]
        public void GetProductById_WhenIdIs1_ThenReturnProduct123()
        {
            // Arrange
            var productService = new ProductService(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
            _mockRepository.Setup(x => x.GetProductById(1)).Returns(_products.First(y=> y.Id == 1));

            // Act
            var result = productService.GetProductById(1);

            // Assert
            Assert.That(result.ProductNumber, Is.EqualTo("123"));
            _mockRepository.Verify(repo => repo.GetProductById(1), Times.Once);
        }

        [Test]
        public void GetProductById_WhenIdDoesNotExists_ThenReturnNull()
        {
            // Arrange
            var productService = new ProductService(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
            _mockRepository.Setup(x => x.GetProductById(100)).Returns(_products.FirstOrDefault(y => y.Id == 100));

            // Act
            var result = productService.GetProductById(100);

            // Assert
            Assert.IsNull(result);
            _mockRepository.Verify(repo => repo.GetProductById(100), Times.Once);
        }

        [Test]
        public void UpdateProduct_WhenIdExists_ThenUpdateProductWasCalled()
        {
            // Arrange
            var productService = new ProductService(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
            var model = new Product { ProductNumber = "111", Name = "Product D updated", Price = 14 };
            var mockProduct = _products.First(y => y.Id == 2);
            _mockMapper.Setup(mapper => mapper.Map(model, mockProduct)).Returns(mockProduct);
            _mockRepository.Setup(x => x.GetProductById(2)).Returns(mockProduct);

            // Act
            productService.UpdateProduct(2, model);

            // Assert
            _mockRepository.Verify(repo => repo.UpdateProduct(mockProduct), Times.Once);
        }

        [Test]
        public void DeleteProduct_WhenIdExists_ThenReturnUpdatedProduct()
        {
            // Arrange
            var productService = new ProductService(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
            var mockProduct = _products.First(y => y.Id == 2);
            _mockRepository.Setup(x => x.GetProductById(2)).Returns(mockProduct);

            // Act
            productService.DeleteProduct(2);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteProduct(mockProduct), Times.Once);
        }


        private IEnumerable<ProductEntity> MockProducts()
        {
            var products = new List<ProductEntity>
            {
                new ProductEntity {Id = 1, ProductNumber = "123", Name = "Test Product", Price = 10 },
                new ProductEntity {Id = 2, ProductNumber = "111", Name = "Product A", Price = 11 },
                new ProductEntity {Id = 3, ProductNumber = "222", Name = "Product B", Price = 12 },
                new ProductEntity {Id = 4, ProductNumber = "333", Name = "Product C", Price = 13 }
            };
            return products;
        }
    }
}