using Domain;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AddCompany([FromBody] List<Company> company)
        {
            if (company != null)
            {
                var companies = new List<Company>();
                foreach (Company i in company)
                {
                    try
                    {
                        companies.Add(await _companiesService.AddCompany(i));
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                return Ok(companies);

            }
            return null;
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

        [HttpGet]
        [Route("companies/search/{search}")]
        public async Task<IActionResult> GetCompaniesBySearch(string search)
        {
            try
            {
                return Ok(await _companiesService.GetCompaniesBySearch(search));
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
