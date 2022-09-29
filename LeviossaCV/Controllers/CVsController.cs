using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Utility.Interface;

namespace LeviossaCV.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin, Manager")]
    public class CVsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public CVsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [Route("CV/add")]
        public async Task<IActionResult> AddCV([FromBody] CVDTO cv)
        {
            try
            {
                return Ok(await _serviceManager.CVsService.AddCV(cv));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("CVs/{id}")]
        public async Task<IActionResult> UpdateCV([FromBody] CVDTO cv, int id)
        {
            try
            {
                cv.Id = id;
                return Ok(await _serviceManager.CVsService.UpdateCV(cv));
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
                return Ok(await _serviceManager.CVsService.GetAllCVs());
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
                return Ok(await _serviceManager.CVsService.GetCVById(id));
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
                return Ok(await _serviceManager.CVsService.GetCVsBySearch(search));
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
                await _serviceManager.CVsService.DeleteCVById(id);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }       
}