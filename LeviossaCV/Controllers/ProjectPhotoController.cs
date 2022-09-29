using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Utility.Interface;

namespace LeviossaCV.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin, Manager")]
    public class ProjectPhotoController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public ProjectPhotoController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [Route("projectphoto/add")]
        public async Task<IActionResult> AddProjectPhoto(IFormFile image)
        {
            try
            {
                return Ok(await _serviceManager.ProjectPhotoService.AddProjectPhoto(image));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("projectphoto/{id}")]
        public async Task<IActionResult> DeleteProjectPhotoById(int id)
        {
            try
            {
                await _serviceManager.ProjectPhotoService.DeleteProjectPhotoById(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}