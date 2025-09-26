using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FunctionAppDemo.Models;
using FunctionAppDemo.Repositories;
using FunctionAppDemo.Services;
using Moq;
using Xunit;

namespace FunctionAppDemo.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _service = new ProductService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetProductAsync_ReturnsProduct_WhenExists()
        {
            var id = Guid.NewGuid();
            var product = new Product { Id = id, Name = "Test", Price = 10, StoreId = Guid.NewGuid() };
            _repositoryMock.Setup(r => r.GetAsync(id)).ReturnsAsync(product);

            var result = await _service.GetProductAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result!.Id);
        }

        [Fact]
        public async Task GetProductAsync_ReturnsNull_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _repositoryMock.Setup(r => r.GetAsync(id)).ReturnsAsync((Product?)null);

            var result = await _service.GetProductAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsProducts()
        {
            var products = new List<Product> { new Product { Id = Guid.NewGuid(), Name = "A", Price = 1, StoreId = Guid.NewGuid() } };
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            var result = await _service.GetAllProductsAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task CreateProductAsync_ReturnsCreatedProduct()
        {
            var product = new Product { Name = "New", Price = 5, StoreId = Guid.NewGuid() };
            var created = new Product { Id = Guid.NewGuid(), Name = "New", Price = 5, StoreId = product.StoreId };
            _repositoryMock.Setup(r => r.CreateAsync(product)).ReturnsAsync(created);

            var result = await _service.CreateProductAsync(product);

            Assert.NotNull(result);
            Assert.Equal(created.Name, result.Name);
        }

        [Fact]
        public async Task UpdateProductAsync_ReturnsUpdatedProduct_WhenExists()
        {
            var product = new Product { Id = Guid.NewGuid(), Name = "Update", Price = 20, StoreId = Guid.NewGuid() };
            _repositoryMock.Setup(r => r.UpdateAsync(product)).ReturnsAsync(product);

            var result = await _service.UpdateProductAsync(product);

            Assert.NotNull(result);
            Assert.Equal(product.Name, result!.Name);
        }

        [Fact]
        public async Task UpdateProductAsync_ReturnsNull_WhenNotExists()
        {
            var product = new Product { Id = Guid.NewGuid(), Name = "Update", Price = 20, StoreId = Guid.NewGuid() };
            _repositoryMock.Setup(r => r.UpdateAsync(product)).ReturnsAsync((Product?)null);

            var result = await _service.UpdateProductAsync(product);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteProductAsync_ReturnsTrue_WhenDeleted()
        {
            var id = Guid.NewGuid();
            _repositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);

            var result = await _service.DeleteProductAsync(id);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteProductAsync_ReturnsFalse_WhenNotDeleted()
        {
            var id = Guid.NewGuid();
            _repositoryMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

            var result = await _service.DeleteProductAsync(id);

            Assert.False(result);
        }
    }
}
