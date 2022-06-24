using Domain;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace LeviossaCV.Controllers
{
    [ApiController]
    public class ProjectPhotoController : ControllerBase
    {
        private readonly IProjectPhotoService _projectPhotoService;
        public ProjectPhotoController(IServiceProvider _serviceProvider)
        {
            _projectPhotoService = _serviceProvider.GetService<IProjectPhotoService>();
        }

        [HttpPost]
        [Route("projectphoto/add")]
        public async Task<IActionResult> AddProjectPhoto(IFormFile image)
        {
            try
            {
                return Ok(await _projectPhotoService.AddProjectPhoto(image));
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
                return Ok(await _projectPhotoService.DeleteProjectPhotoById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}