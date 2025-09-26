using WebApplicationDemo.Models;

namespace WebApplicationDemo.Repositories.Interfaces
{
    public interface IStoreRepository
    {
        Task<Store?> GetAsync(Guid id);
        Task<IEnumerable<Store>> GetAllAsync();
        Task<Store> CreateAsync(Store store);
        Task<Store?> UpdateAsync(Store store);
        Task<bool> DeleteAsync(Guid id);
    }
}
