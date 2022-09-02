using AutoMapper;
using Domain;
using LeviossaCV.UI;
using Microsoft.AspNetCore.Mvc;
using Services.Utility.Interface;

namespace API.Controllers
{
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        public CompaniesController(IMapper mapper, IServiceManager serviceManager)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }

        public class AppMappingCompanyController : Profile
        {
            public AppMappingCompanyController()
            {
                CreateMap<CompanyDTO, SimpleElementUI>().ReverseMap();
            }
        }

        [HttpPost]
        [Route("companies/add")]
        public async Task<IActionResult> AddCompany([FromBody] List<CompanyDTO> company)
        {
            try
            {
                var companiesDTO = await _serviceManager.CompaniesService.AddCompanies(company);
                return Ok(_mapper.Map<List<SimpleElementUI>>(companiesDTO));
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

                return Ok(_mapper.Map<SimpleElementUI>(await _serviceManager.CompaniesService.UpdateCompany(company)));
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
                var companiesDTO = (await _serviceManager.CompaniesService.GetAllCompanies());
                return Ok(_mapper.Map<List<SimpleElementUI>>(companiesDTO));
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
                return Ok(_mapper.Map<SimpleElementUI>(await _serviceManager.CompaniesService.GetCompanyById(id)));
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
                var companiesDTO = await _serviceManager.CompaniesService.GetCompaniesBySearch(search);
                return Ok(_mapper.Map<List<SimpleElementUI>>(companiesDTO));
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

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
