using WebApplicationDemo.Data;
using WebApplicationDemo.Models;
using Microsoft.EntityFrameworkCore;
using WebApplicationDemo.Repositories.Interfaces;

namespace WebApplicationDemo.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AppDbContext _context;

        public StoreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Store?> GetAsync(Guid id)
        {
            return await _context.Stores.FindAsync(id);
        }

        public async Task<IEnumerable<Store>> GetAllAsync()
        {
            return await _context.Stores.ToListAsync();
        }

        public async Task<Store> CreateAsync(Store store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task<Store?> UpdateAsync(Store store)
        {
            var existing = await _context.Stores.FindAsync(store.Id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(store);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return false;
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
