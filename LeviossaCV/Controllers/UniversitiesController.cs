using AutoMapper;
using Domain;
using LeviossaCV.UI;
using Microsoft.AspNetCore.Mvc;
using Services.Utility.Interface;

namespace API.Controllers
{
    [ApiController]
    public class UniversitiesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        public UniversitiesController(IMapper mapper, IServiceManager serviceManager)
        {
            _mapper = mapper;
            _serviceManager = serviceManager;
        }

        public class AppMappingUniversityController : Profile
        {
            public AppMappingUniversityController()
            {
                CreateMap<UniversityDTO, SimpleElementUI>().ReverseMap();
            }
        }

        [HttpPost]
        [Route("universities/add")]
        public async Task<IActionResult> AddUniversity([FromBody] List<UniversityDTO> university)
        {
            try
            {
                var universitiesDTO = await _serviceManager.UniversitiesService.AddUniversities(university);
                return Ok(_mapper.Map<List<SimpleElementUI>>(universitiesDTO));
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

                return Ok(_mapper.Map<SimpleElementUI>(await _serviceManager.UniversitiesService.UpdateUniversity(university)));
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
                var universitiesDTO = await _serviceManager.UniversitiesService.GetAllUniversities();
                return Ok(_mapper.Map<List<SimpleElementUI>>(universitiesDTO));
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
                return Ok(_mapper.Map<SimpleElementUI>(await _serviceManager.UniversitiesService.GetUniversityById(id)));
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
                var universitiesDTO = await _serviceManager.UniversitiesService.GetUniversitiesBySearch(search);
                return Ok(_mapper.Map<List<SimpleElementUI>>(universitiesDTO));
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

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
