using Microsoft.AspNetCore.Mvc;
using WebApplicationDemo.DTOs;
using WebApplicationDemo.Models;
using WebApplicationDemo.Services.Interfaces;

namespace WebApplicationDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStore(Guid id)
        {
            var store = await _storeService.GetStoreAsync(id);
            if (store == null) return NotFound();
            var response = new StoreResponse { Id = store.Id, Name = store.Name, CompanyId = store.CompanyId };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStores()
        {
            var stores = await _storeService.GetAllStoresAsync();
            var responses = stores.Select(s => new StoreResponse { Id = s.Id, Name = s.Name, CompanyId = s.CompanyId });
            return Ok(responses);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody] StoreCreateRequest request)
        {
            var store = new Store { Name = request.Name, CompanyId = request.CompanyId };
            var created = await _storeService.CreateStoreAsync(store);
            var response = new StoreResponse { Id = created.Id, Name = created.Name, CompanyId = created.CompanyId };
            return CreatedAtAction(nameof(GetStore), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(Guid id, [FromBody] StoreUpdateRequest request)
        {
            var store = new Store { Id = id, Name = request.Name, CompanyId = request.CompanyId };
            var updated = await _storeService.UpdateStoreAsync(store);
            if (updated == null) return NotFound();
            var response = new StoreResponse { Id = updated.Id, Name = updated.Name, CompanyId = updated.CompanyId };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(Guid id)
        {
            var deleted = await _storeService.DeleteStoreAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
