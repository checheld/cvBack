using Microsoft.AspNetCore.Mvc;
using Services.Domain;
using Services.Utility.Interface;

namespace LeviossaCV.Controllers
{
    [ApiController]
    public class ProjectTypesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ProjectTypesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [Route("projectTypes/add")]
        public async Task<IActionResult> AddProjectTypes([FromBody] List<ProjectTypeDTO> projectType)
        {
            try
            {
                return Ok(await _serviceManager.ProjectTypesService.AddProjectTypes(projectType));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("projectTypes/{id}")]
        public async Task<IActionResult> UpdateProjectType([FromBody] ProjectTypeDTO projectType, int id)
        {
            try
            {
                projectType.Id = id;

                return Ok(await _serviceManager.ProjectTypesService.UpdateProjectType(projectType));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("projectTypes")]
        public async Task<IActionResult> GetAllProjectTypes()
        {
            try
            {
                return Ok(await _serviceManager.ProjectTypesService.GetAllProjectTypes());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("projectTypes/{id}")]
        public async Task<IActionResult> GetProjectTypeById(int id)
        {
            try
            {
                return Ok(await _serviceManager.ProjectTypesService.GetProjectTypeById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("projectTypes/search/{search}")]
        public async Task<IActionResult> GetProjectTypesBySearch(string search)
        {
            try
            {
                return Ok(await _serviceManager.ProjectTypesService.GetProjectTypesBySearch(search));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("projectTypes/{id}")]
        public async Task<IActionResult> DeleteProjectTypeById(int id)
        {
            try
            {
                await _serviceManager.ProjectTypesService.DeleteProjectTypeById(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}