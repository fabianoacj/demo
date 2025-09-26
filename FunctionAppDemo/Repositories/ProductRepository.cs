using System.Data;
using System.Text.Json;
using Dapper;
using FunctionAppDemo.Models;

namespace FunctionAppDemo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProductRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Product?> GetAsync(Guid id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var sql = "SELECT dbo.GetProductsJson()";
            var json = await _dbConnection.ExecuteScalarAsync<string>(sql);
            if (string.IsNullOrWhiteSpace(json))
                return new List<Product>();
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            product.Id = Guid.NewGuid();
            var sql = "InsertProduct";
            var parameters = new
            {
                product.Id,
                product.Name,
                product.Price,
                product.StoreId
            };
            await _dbConnection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
            return product;
        }

        public async Task<Product?> UpdateAsync(Product product)
        {
            var sql = @"UPDATE Products SET Name = @Name, Price = @Price, StoreId = @StoreId WHERE Id = @Id";
            var affected = await _dbConnection.ExecuteAsync(sql, product);
            return affected > 0 ? product : null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";
            var affected = await _dbConnection.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }
    }
}
