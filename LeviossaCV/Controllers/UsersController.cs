using Domain;
using Microsoft.AspNetCore.Mvc;
using Services.Utility.Interface;

namespace LeviossaCV.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public UsersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [Route("user/add")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO user)
        {
            try
            {
                return Ok(await _serviceManager.UsersService.AddUser(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("users/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO user, int id)
        {
            user.Id = id;
            try
            {
                return Ok(await _serviceManager.UsersService.UpdateUser(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _serviceManager.UsersService.GetAllUsers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("users/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                return Ok(await _serviceManager.UsersService.GetUserById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("users/search/{search}")]
        public async Task<IActionResult> GetUsersBySearch(string search)
        {
            try
            {
                return Ok(await _serviceManager.UsersService.GetUsersBySearch(search));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("users/{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            try
            {
                await _serviceManager.UsersService.DeleteUserById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}