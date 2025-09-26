using WebApplicationDemo.Data;
using WebApplicationDemo.Models;
using Microsoft.EntityFrameworkCore;
using WebApplicationDemo.Repositories.Interfaces;

namespace WebApplicationDemo.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Company?> GetAsync(Guid id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> CreateAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<Company?> UpdateAsync(Company company)
        {
            var existing = await _context.Companies.FindAsync(company.Id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(company);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null) return false;
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
