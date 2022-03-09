using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstract;

namespace API.Controllers
{
    [ApiController]
    public class UniversitiesController : ControllerBase
    {
        private readonly IUniversitiesService _universitiesService;
        public UniversitiesController(IServiceProvider _serviceProvider)
        {
            _universitiesService = _serviceProvider.GetService<IUniversitiesService>();
        }

        [HttpPost]
        [Route("universities/add")]
        public async Task<IActionResult> AddUniversity([FromBody] University university)
        {
            try
            {
                return Ok(await _universitiesService.AddUniversity(university));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("universities/{id}")]
        public async Task<IActionResult> UpdateUniversity([FromBody] University university, int id)
        {
            university.Id = id;
            try
            {
                return Ok(await _universitiesService.UpdateUniversity(university));
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
                return Ok(await _universitiesService.GetAllUniversities());
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
                return Ok(await _universitiesService.GetUniversityById(id));
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
                return Ok(await _universitiesService.DeleteUniversityById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
