using Microsoft.AspNetCore.Mvc;
using WebApplicationDemo.DTOs;
using WebApplicationDemo.Models;
using WebApplicationDemo.Services.Interfaces;

namespace WebApplicationDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _companyService.GetCompanyAsync(id);
            if (company == null) return NotFound();
            var response = new CompanyResponse { Id = company.Id, Name = company.Name };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            var responses = companies.Select(c => new CompanyResponse { Id = c.Id, Name = c.Name });
            return Ok(responses);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreateRequest request)
        {
            var company = new Company { Name = request.Name };
            var created = await _companyService.CreateCompanyAsync(company);
            var response = new CompanyResponse { Id = created.Id, Name = created.Name };
            return CreatedAtAction(nameof(GetCompany), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyUpdateRequest request)
        {
            var company = new Company { Id = id, Name = request.Name };
            var updated = await _companyService.UpdateCompanyAsync(company);
            if (updated == null) return NotFound();
            var response = new CompanyResponse { Id = updated.Id, Name = updated.Name };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var deleted = await _companyService.DeleteCompanyAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
