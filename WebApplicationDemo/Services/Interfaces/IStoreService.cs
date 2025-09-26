using WebApplicationDemo.Models;

namespace WebApplicationDemo.Services.Interfaces
{
    public interface IStoreService
    {
        Task<Store?> GetStoreAsync(Guid id);
        Task<IEnumerable<Store>> GetAllStoresAsync();
        Task<Store> CreateStoreAsync(Store store);
        Task<Store?> UpdateStoreAsync(Store store);
        Task<bool> DeleteStoreAsync(Guid id);
    }
}
