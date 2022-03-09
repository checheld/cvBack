using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;

namespace API.Controllers
{
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompaniesService _companiesService;
        public CompaniesController(IServiceProvider _serviceProvider)
        {
            _companiesService = _serviceProvider.GetService<ICompaniesService>();
        }

        [HttpPost]
        [Route("companies/add")]
        public async Task<IActionResult> AddCompany([FromBody] Company company)
        {
            try
            {
                return Ok(await _companiesService.AddCompany(company));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("companies/{id}")]
        public async Task<IActionResult> UpdateCompany([FromBody] Company company, int id)
        {
            company.Id = id;
            try
            {
                return Ok(await _companiesService.UpdateCompany(company));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("companies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                return Ok(await _companiesService.GetAllCompanies());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("companies/{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            try
            {
                return Ok(await _companiesService.GetCompanyById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("companies/{id}")]
        public async Task<IActionResult> DeleteCompanyById(int id)
        {
            try
            {
                return Ok(await _companiesService.DeleteCompanyById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
