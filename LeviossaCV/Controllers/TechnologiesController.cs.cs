using Domain;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace LeviossaCV.Controllers
{
    [ApiController]
    public class TechnologiesController : ControllerBase
    {
        private readonly ITechnologiesService _technologiesService;
        public TechnologiesController(IServiceProvider _serviceProvider)
        {
            _technologiesService = _serviceProvider.GetService<ITechnologiesService>();
        }

        [HttpPost]
        [Route("technologies/add")]
        public async Task<IActionResult> AddTechnology([FromBody] TechnologyDTO technology)
        {
            try
            {
                return Ok(await _technologiesService.AddTechnology(technology));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("technologies/{id}")]
        public async Task<IActionResult> UpdateTechnology([FromBody] TechnologyDTO technology, int id)
        {
            technology.Id = id;
            try
            {
                return Ok(await _technologiesService.UpdateTechnology(technology));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("technologies")]
        public async Task<IActionResult> GetAllTechnologies()
        {
            try
            {
                return Ok(await _technologiesService.GetAllTechnologies());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("technologies/{id}")]
        public async Task<IActionResult> GetTechnologyById(int id)
        {
            try
            {
                return Ok(await _technologiesService.GetTechnologyById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("technologies/search/{search}")]
        public async Task<IActionResult> GetTechnologiesBySearch(string search)
        {
            try
            {
                return Ok(await _technologiesService.GetTechnologiesBySearch(search));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("technologies/{id}")]
        public async Task<IActionResult> DeleteTechnologyById(int id)
        {
            try
            {
                return Ok(await _technologiesService.DeleteTechnologyById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}