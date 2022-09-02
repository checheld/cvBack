using AutoMapper;
using LeviossaCV.UI;
using Microsoft.AspNetCore.Mvc;
using Services.Domain;
using Services.Utility.Interface;

namespace LeviossaCV.Controllers
{
    [ApiController]
    public class ProjectTypesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        public ProjectTypesController(IMapper mapper, IServiceManager serviceManager)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }

        public class AppMappingProjectTypeController : Profile
        {
            public AppMappingProjectTypeController()
            {
                CreateMap<ProjectTypeDTO, SimpleElementUI>().ReverseMap();
            }
        }

        [HttpPost]
        [Route("projectTypes/add")]
        public async Task<IActionResult> AddProjectTypes([FromBody] List<ProjectTypeDTO> projectType)
        {
            try
            {
                var projectTypeDTO = await _serviceManager.ProjectTypesService.AddProjectTypes(projectType);
                return Ok(_mapper.Map<List<SimpleElementUI>>(projectTypeDTO));
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

                return Ok(_mapper.Map<SimpleElementUI>(await _serviceManager.ProjectTypesService.UpdateProjectType(projectType)));
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
                var projectTypesDTO = await _serviceManager.ProjectTypesService.GetAllProjectTypes();
                return Ok(_mapper.Map<List<SimpleElementUI>>(projectTypesDTO));
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
                return Ok(_mapper.Map<SimpleElementUI>(await _serviceManager.ProjectTypesService.GetProjectTypeById(id)));
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
                var projectTypeDTO = await _serviceManager.ProjectTypesService.GetProjectTypesBySearch(search);
                return Ok(_mapper.Map<List<SimpleElementUI>>(projectTypeDTO));
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

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}