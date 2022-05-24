using Domain;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace LeviossaCV.Controllers
{
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;
        public ProjectsController(IServiceProvider _serviceProvider)
        {
            _projectsService = _serviceProvider.GetService<IProjectsService>();
        }

        [HttpPost]
        [Route("projects/add")]

        public async Task<IActionResult> AddProject([FromBody] ProjectDTO project)
        {
            try
            {
                return Ok(await _projectsService.AddProject(project));
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
            project.Id = id;
            try
            {
                return Ok(await _projectsService.UpdateProject(project));
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
                return Ok(await _projectsService.GetAllProjects());
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
                return Ok(await _projectsService.GetProjectById(id));
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
                return Ok(await _projectsService.GetProjectsBySearch(searchProjects));
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
                return Ok(await _projectsService.DeleteProjectById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}