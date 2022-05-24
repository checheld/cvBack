using Domain;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace LeviossaCV.Controllers
{
    [ApiController]
    public class ProfilePhotoController : ControllerBase
    {
        private readonly IProfilePhotoService _profilePhotoService;
        public ProfilePhotoController(IServiceProvider _serviceProvider)
        {
            _profilePhotoService = _serviceProvider.GetService<IProfilePhotoService>();
        }

        [HttpPost]
        [Route("profilephoto/add")]
        public async Task<IActionResult> AddProfilePhoto(IFormFile image)
        {
            try
            {
                return Ok(await _profilePhotoService.AddProfilePhoto(image));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}