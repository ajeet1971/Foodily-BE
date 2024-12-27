using Foodily.Interface;
using Foodily.Services;
using Foodily.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Foodily.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var result = await _userService.GetUserListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetAllUsersById(int id)
        {
            try
            {
                var result = await _userService.GetUserById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> AddUserDetails([FromBody] UserVM users)
        {
            try
            {
                var res = await _userService.AddUserDetails(users);
                if (res == 1)
                    return StatusCode(StatusCodes.Status200OK, "Inserted Successfully.");
                else if (res == -1)
                    return StatusCode(StatusCodes.Status400BadRequest, "Email already exists.");
                else
                    return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] User userDto)
        {
            var user = await _userService.GetUser(userDto.Email, userDto.Password);
            if (user == null)
                return Unauthorized();

            var token = _userService.GenerateToken(user);
            return StatusCode(StatusCodes.Status200OK, new {Token = token,  message = "Login Successful." });
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok("Test");
        //}

    }
}
