using WebApplicationDemo.Models;

namespace WebApplicationDemo.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<Company?> GetCompanyAsync(Guid id);
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company> CreateCompanyAsync(Company company);
        Task<Company?> UpdateCompanyAsync(Company company);
        Task<bool> DeleteCompanyAsync(Guid id);
    }
}
