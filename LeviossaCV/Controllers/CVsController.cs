using Domain;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace LeviossaCV.Controllers
{
    [ApiController]
    public class CVsController : ControllerBase
    {
        private readonly ICVsService _CVsService;
        public CVsController(IServiceProvider _serviceProvider)
        {
            _CVsService = _serviceProvider.GetService<ICVsService>();
        }

        [HttpPost]
        [Route("CV/add")]
        public async Task<IActionResult> AddCV([FromBody] CVDTO cv)
        {
            try
            {
                return Ok(await _CVsService.AddCV(cv));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("CVs/{id}")]
        public async Task<IActionResult> UpdateCV([FromBody] CVDTO /*CVModelUpate*/ cv, int id)
        {
            cv.Id = id;
            try
            {
                return Ok(await _CVsService.UpdateCV(cv));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("CVs")]
        public async Task<IActionResult> GetAllCVs()
        {
            try
            {
                return Ok(await _CVsService.GetAllCVs());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("CVs/{id}")]
        public async Task<IActionResult> GetCVById(int id)
        {
            try
            {
                return Ok(await _CVsService.GetCVById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("CVs/search/{search}")]
        public async Task<IActionResult> GetCVsBySearch(string search)
        {
            try
            {
                return Ok(await _CVsService.GetCVsBySearch(search));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("CVs/{id}")]
        public async Task<IActionResult> DeleteCVById(int id)
        {
            try
            {
                return Ok(await _CVsService.DeleteCVById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }       
}