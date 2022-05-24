using Domain;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AddUniversity([FromBody] List<UniversityDTO> university)
        {
            if (university != null)
            {
                var universities = new List<UniversityDTO>();
                foreach (UniversityDTO i in university)
                {
                    try
                    {
                        universities.Add(await _universitiesService.AddUniversity(i));
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                return Ok(universities);
            }
            return null;
        }

        [HttpPut]
        [Route("universities/{id}")]
        public async Task<IActionResult> UpdateUniversity([FromBody] UniversityDTO university, int id)
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

        [HttpGet]
        [Route("universities/search/{search}")]
        public async Task<IActionResult> GetUniversitiesBySearch(string search)
        {
            try
            {
                return Ok(await _universitiesService.GetUniversitiesBySearch(search));
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
