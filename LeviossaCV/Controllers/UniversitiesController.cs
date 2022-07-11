using Domain;
using Microsoft.AspNetCore.Mvc;
using Services.Utility.Interface;

namespace API.Controllers
{
    [ApiController]
    public class UniversitiesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public UniversitiesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [Route("universities/add")]
        public async Task<IActionResult> AddUniversity([FromBody] List<UniversityDTO> university)
        {
            try
            {
                return Ok(await _serviceManager.UniversitiesService.AddUniversities(university));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("universities/{id}")]
        public async Task<IActionResult> UpdateUniversity([FromBody] UniversityDTO university, int id)
        {
            try
            {
                university.Id = id;

                return Ok(await _serviceManager.UniversitiesService.UpdateUniversity(university));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("universities")]
        public async Task<IActionResult> GetAllUniversities()
        {
            try
            {
                return Ok(await _serviceManager.UniversitiesService.GetAllUniversities());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("universities/{id}")]
        public async Task<IActionResult> GetUniversityById(int id)
        {
            try
            {
                return Ok(await _serviceManager.UniversitiesService.GetUniversityById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("universities/search/{search}")]
        public async Task<IActionResult> GetUniversitiesBySearch(string search)
        {
            try
            {
                return Ok(await _serviceManager.UniversitiesService.GetUniversitiesBySearch(search));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("universities/{id}")]
        public async Task<IActionResult> DeleteUniversityById(int id)
        {
            try
            {
                await _serviceManager.UniversitiesService.DeleteUniversityById(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
