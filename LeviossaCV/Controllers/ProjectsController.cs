using AutoMapper;
using Domain;
using LeviossaCV.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Utility.Interface;

namespace LeviossaCV.Controllers
{
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        public ProjectsController(IMapper mapper, IServiceManager serviceManager)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }

        public class AppMappingProjectsController : Profile
        {
            public AppMappingProjectsController()
            {
                CreateMap<ProjectDTO, ProjectUI>().ForMember(x => x.TechnologyList, y => y.MapFrom(t => t.TechnologyList)).
                    ForMember(x => x.PhotoList, y => y.MapFrom(t => t.PhotoList)).ReverseMap();
            }
        }

        [HttpPost]
        [Route("projects/add")]

        public async Task<IActionResult> AddProject([FromBody] ProjectDTO project)
        {
            try
            {
                var projectDTO = await _serviceManager.ProjectsService.AddProject(project);
                return Ok(_mapper.Map<ProjectUI>(projectDTO));
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

                return Ok(_mapper.Map<ProjectUI>(await _serviceManager.ProjectsService.UpdateProject(project)));
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
                var projectsDTO = await _serviceManager.ProjectsService.GetAllProjects();
                return Ok(_mapper.Map<List<ProjectUI>>(projectsDTO));
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
                return Ok(_mapper.Map<ProjectUI>(await _serviceManager.ProjectsService.GetProjectById(id)));
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
                return Ok(_mapper.Map<List<ProjectUI>>(await _serviceManager.ProjectsService.GetProjectsBySearch(searchProjects)));
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

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}