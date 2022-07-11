using Domain;
using Microsoft.AspNetCore.Mvc;
using Services.Utility.Interface;

namespace API.Controllers
{
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public CompaniesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [Route("companies/add")]
        public async Task<IActionResult> AddCompany([FromBody] List<CompanyDTO> company)
        {
            try
            {
                return Ok(await _serviceManager.CompaniesService.AddCompanies(company));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("companies/{id}")]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyDTO company, int id)
        {
            try
            {
                company.Id = id;

                return Ok(await _serviceManager.CompaniesService.UpdateCompany(company));
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
                return Ok(await _serviceManager.CompaniesService.GetAllCompanies());
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
                return Ok(await _serviceManager.CompaniesService.GetCompanyById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("companies/search/{search}")]
        public async Task<IActionResult> GetCompaniesBySearch(string search)
        {
            try
            {
                return Ok(await _serviceManager.CompaniesService.GetCompaniesBySearch(search));
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
                await _serviceManager.CompaniesService.DeleteCompanyById(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
