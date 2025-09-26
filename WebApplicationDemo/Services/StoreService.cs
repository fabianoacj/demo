using WebApplicationDemo.Models;
using WebApplicationDemo.Repositories.Interfaces;
using WebApplicationDemo.Services.Interfaces;

namespace WebApplicationDemo.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _repository;

        public StoreService(IStoreRepository repository)
        {
            _repository = repository;
        }

        public Task<Store?> GetStoreAsync(Guid id) => _repository.GetAsync(id);
        public Task<IEnumerable<Store>> GetAllStoresAsync() => _repository.GetAllAsync();
        public Task<Store> CreateStoreAsync(Store store) => _repository.CreateAsync(store);
        public Task<Store?> UpdateStoreAsync(Store store) => _repository.UpdateAsync(store);
        public Task<bool> DeleteStoreAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
