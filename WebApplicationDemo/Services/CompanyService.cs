using WebApplicationDemo.Models;
using WebApplicationDemo.Repositories.Interfaces;
using WebApplicationDemo.Services.Interfaces;

namespace WebApplicationDemo.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;

        public CompanyService(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public Task<Company?> GetCompanyAsync(Guid id) => _repository.GetAsync(id);
        public Task<IEnumerable<Company>> GetAllCompaniesAsync() => _repository.GetAllAsync();
        public Task<Company> CreateCompanyAsync(Company company) => _repository.CreateAsync(company);
        public Task<Company?> UpdateCompanyAsync(Company company) => _repository.UpdateAsync(company);
        public Task<bool> DeleteCompanyAsync(Guid id) => _repository.DeleteAsync(id);
    }
}
