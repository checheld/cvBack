using AutoMapper;
using Domain;
using LeviossaCV.UI;
using Microsoft.AspNetCore.Mvc;
using Services.Utility.Interface;

namespace LeviossaCV.Controllers
{
    [ApiController]
    public class TechnologiesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        public TechnologiesController(IMapper mapper, IServiceManager serviceManager)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;

        }

        public class AppMappingTechnologyController : Profile
        {
            public AppMappingTechnologyController()
            {
                CreateMap<TechnologyDTO, TechnologyUI>().ReverseMap();
            }
        }

        [HttpPost]
        [Route("technologies/add")]
        public async Task<IActionResult> AddTechnology([FromBody] List<TechnologyDTO> technology)
        {
            try
            {
                var techDTO = await _serviceManager.TechnologiesService.AddTechnology(technology);
                return Ok(_mapper.Map<List<TechnologyUI>>(techDTO));
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

                return Ok(_mapper.Map<TechnologyUI>(await _serviceManager.TechnologiesService.UpdateTechnology(technology)));
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
                var techDTO = await _serviceManager.TechnologiesService.GetAllTechnologies();
                return Ok(_mapper.Map<List<TechnologyUI>>(techDTO));
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
                return Ok(_mapper.Map<TechnologyUI>(await _serviceManager.TechnologiesService.GetTechnologyById(id)));
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
                var techDTO = await _serviceManager.TechnologiesService.GetTechnologiesBySearch(search);

                return Ok(_mapper.Map<List<TechnologyUI>>(techDTO));
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

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}