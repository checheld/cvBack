using Domain;
using Microsoft.AspNetCore.Mvc;
using Services.Utility.Interface;

namespace LeviossaCV.Controllers
{
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ProjectsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [Route("projects/add")]

        public async Task<IActionResult> AddProject([FromBody] ProjectDTO project)
        {
            try
            {
                return Ok(await _serviceManager.ProjectsService.AddProject(project));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("projects/{id}")]
        public async Task<IActionResult> UpdateProject([FromBody] ProjectDTO project, int id)
        {
            try
            {
                project.Id = id;

                return Ok(await _serviceManager.ProjectsService.UpdateProject(project));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("projects")]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                return Ok(await _serviceManager.ProjectsService.GetAllProjects());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("projects/project/{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            try
            {
                return Ok(await _serviceManager.ProjectsService.GetProjectById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("projects/search")]
        public async Task<IActionResult> GetProjectsBySearch([FromBody] SearchProjectsDTO searchProjects)
        {
            try
            {
                return Ok(await _serviceManager.ProjectsService.GetProjectsBySearch(searchProjects));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("projects/{id}")]
        public async Task<IActionResult> DeleteProjectById(int id)
        {
            try
            {
                await _serviceManager.ProjectsService.DeleteProjectById(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}