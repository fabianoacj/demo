using WebApplicationDemo.Models;

namespace WebApplicationDemo.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company?> GetAsync(Guid id);
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> CreateAsync(Company company);
        Task<Company?> UpdateAsync(Company company);
        Task<bool> DeleteAsync(Guid id);
    }
}
