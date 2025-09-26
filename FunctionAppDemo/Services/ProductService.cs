using FunctionAppDemo.Models;
using FunctionAppDemo.Repositories;

namespace FunctionAppDemo.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task<Product?> GetProductAsync(Guid id) => _repository.GetAsync(id);
        public Task<IEnumerable<Product>> GetAllProductsAsync() => _repository.GetAllAsync();
        public Task<Product> CreateProductAsync(Product product) => _repository.CreateAsync(product);
        public Task<Product?> UpdateProductAsync(Product product) => _repository.UpdateAsync(product);
        public Task<bool> DeleteProductAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
