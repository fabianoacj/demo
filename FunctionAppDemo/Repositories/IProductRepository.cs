using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FunctionAppDemo.Models;

namespace FunctionAppDemo.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> CreateAsync(Product product);
        Task<Product?> UpdateAsync(Product product);
        Task<bool> DeleteAsync(Guid id);
    }
}
