using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using FunctionAppDemo.Models;
using FunctionAppDemo.Services;
using System.Text.Json;

namespace FunctionAppDemo
{
    public class ProductFunctions
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductFunctions> _logger;

        public ProductFunctions(ILogger<ProductFunctions> logger, IProductService productService)
        {
            _productService = productService;
            _logger = logger;
        }

        [Function("GetProduct")]
        public async Task<IActionResult> GetProduct(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "product/{id}")] HttpRequestData req,
            Guid id)
        {
            try
            {
                var product = await _productService.GetProductAsync(id);
                if (product == null) return new NotFoundResult();
                return new OkObjectResult(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetProduct");
                return new ObjectResult("An unexpected error occurred.") { StatusCode = 500 };
            }
        }

        [Function("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "product")] HttpRequestData req)
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return new OkObjectResult(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetAllProducts");
                return new ObjectResult("An unexpected error occurred.") { StatusCode = 500 };
            }
        }

        [Function("CreateProduct")]
        public async Task<IActionResult> CreateProduct(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "product")] HttpRequestData req)
        {
            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var product = JsonSerializer.Deserialize<Product>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (product == null) return new BadRequestResult();
                var created = await _productService.CreateProductAsync(product);
                return new CreatedResult($"/product/{created.Id}", created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateProduct");
                return new ObjectResult("An unexpected error occurred.") { StatusCode = 500 };
            }
        }

        [Function("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "product/{id}")] HttpRequestData req,
            Guid id)
        {
            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var product = JsonSerializer.Deserialize<Product>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (product == null) return new BadRequestResult();
                product.Id = id;
                var updated = await _productService.UpdateProductAsync(product);
                if (updated == null) return new NotFoundResult();
                return new OkObjectResult(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateProduct");
                return new ObjectResult("An unexpected error occurred.") { StatusCode = 500 };
            }
        }

        [Function("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "product/{id}")] HttpRequestData req,
            Guid id)
        {
            try
            {
                var deleted = await _productService.DeleteProductAsync(id);
                if (!deleted) return new NotFoundResult();
                return new OkResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteProduct");
                return new ObjectResult("An unexpected error occurred.") { StatusCode = 500 };
            }
        }
    }
}
