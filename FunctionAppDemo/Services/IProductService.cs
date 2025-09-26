using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FunctionAppDemo.Models;

namespace FunctionAppDemo.Services
{
    public interface IProductService
    {
        Task<Product?> GetProductAsync(Guid id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> CreateProductAsync(Product product);
        Task<Product?> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Guid id);
    }
}
