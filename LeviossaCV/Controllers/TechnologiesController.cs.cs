using Domain;
using Microsoft.AspNetCore.Mvc;
using Services.Utility.Interface;

namespace LeviossaCV.Controllers
{
    [ApiController]
    public class TechnologiesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public TechnologiesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;

        }

        [HttpPost]
        [Route("technologies/add")]
        public async Task<IActionResult> AddTechnology([FromBody] List<TechnologyDTO> technology)
        {
            try
            {
                return Ok(await _serviceManager.TechnologiesService.AddTechnology(technology));
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
            try
            {
                technology.Id = id;

                return Ok(await _serviceManager.TechnologiesService.UpdateTechnology(technology));
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
                return Ok(await _serviceManager.TechnologiesService.GetAllTechnologies());
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
                return Ok(await _serviceManager.TechnologiesService.GetTechnologyById(id));
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
                return Ok(await _serviceManager.TechnologiesService.GetTechnologiesBySearch(search));
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
                await _serviceManager.TechnologiesService.DeleteTechnologyById(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}