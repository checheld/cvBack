using Domain;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace LeviossaCV.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IServiceProvider _serviceProvider)
        {
            _usersService = _serviceProvider.GetService<IUsersService>();
        }

        [HttpPost]
        [Route("user/add")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO user)
        {
            try
            {
                return Ok(await _usersService.AddUser(user));
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
                return Ok(await _usersService.UpdateUser(user));
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
                return Ok(await _usersService.GetAllUsers());
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
                return Ok(await _usersService.GetUserById(id));
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
                return Ok(await _usersService.GetUsersBySearch(search));
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
                return Ok(await _usersService.DeleteUserById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}