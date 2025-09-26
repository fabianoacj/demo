using Moq;
using WebApplicationDemo.Models;
using WebApplicationDemo.Repositories.Interfaces;
using WebApplicationDemo.Services;

namespace WebApplicationDemo.Tests
{
    public class CompanyServiceTests
    {
        private readonly Mock<ICompanyRepository> _mockRepo;
        private readonly CompanyService _service;

        public CompanyServiceTests()
        {
            _mockRepo = new Mock<ICompanyRepository>();
            _service = new CompanyService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetCompanyAsync_ReturnsCompany_WhenExists()
        {
            var id = Guid.NewGuid();
            var company = new Company { Id = id, Name = "Test" };
            _mockRepo.Setup(r => r.GetAsync(id)).ReturnsAsync(company);

            var result = await _service.GetCompanyAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result!.Id);
        }

        [Fact]
        public async Task GetCompanyAsync_ReturnsNull_WhenNotExists()
        {
            var id = Guid.NewGuid();
            _mockRepo.Setup(r => r.GetAsync(id)).ReturnsAsync((Company?)null);

            var result = await _service.GetCompanyAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllCompaniesAsync_ReturnsCompanies()
        {
            var companies = new List<Company> { new Company { Id = Guid.NewGuid(), Name = "A" } };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(companies);

            var result = await _service.GetAllCompaniesAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task CreateCompanyAsync_CallsRepositoryAndReturnsCompany()
        {
            var company = new Company { Name = "New" };
            _mockRepo.Setup(r => r.CreateAsync(company)).ReturnsAsync(company);

            var result = await _service.CreateCompanyAsync(company);

            Assert.Equal(company.Name, result.Name);
        }

        [Fact]
        public async Task UpdateCompanyAsync_ReturnsUpdatedCompany()
        {
            var company = new Company { Id = Guid.NewGuid(), Name = "Updated" };
            _mockRepo.Setup(r => r.UpdateAsync(company)).ReturnsAsync(company);

            var result = await _service.UpdateCompanyAsync(company);

            Assert.Equal(company.Name, result!.Name);
        }

        [Fact]
        public async Task DeleteCompanyAsync_ReturnsTrue_WhenDeleted()
        {
            var id = Guid.NewGuid();
            _mockRepo.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);

            var result = await _service.DeleteCompanyAsync(id);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteCompanyAsync_ReturnsFalse_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _mockRepo.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

            var result = await _service.DeleteCompanyAsync(id);

            Assert.False(result);
        }
    }
}
