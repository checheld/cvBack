using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Utility.Interface;

namespace LeviossaCV.Controllers
{
    [ApiController]
    [Authorize]
    public class ProfilePhotoController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ProfilePhotoController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [Route("profilephoto/add")]
        public async Task<IActionResult> AddProfilePhoto( IFormFile Image)
        {
            try
            {
                return Ok(await _serviceManager.ProfilePhotoService.AddProfilePhoto(Image));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("profilephoto/params/add")]
        public async Task<IActionResult> AddPhotoParams([FromBody] PhotoParamsDTO photoParams)
        {
            try
            {
                return Ok(await _serviceManager.ProfilePhotoService.AddPhotoParams(photoParams));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("profilephoto/params/{id}")]
        public async Task<IActionResult> UpdatePhotoParams([FromBody] PhotoParamsDTO photoParams, int id)
        {
            try
            {
                photoParams.Id = id;
                return Ok(await _serviceManager.ProfilePhotoService.UpdatePhotoParams(photoParams));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("profilephoto/params/{id}")]
        public async Task<IActionResult> GetPhotoParamsById(int id)
        {
            try
            {
                return Ok(await _serviceManager.ProfilePhotoService.GetPhotoParamsById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}