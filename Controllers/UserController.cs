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
                if (result == null || result.Count == 0)
                { 
                    return NotFound(new { message = "No users found." });
                }
                return Ok(new {Result= result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetAllUsersById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { message = "Invalid user ID." });
                }
                var result = await _userService.GetUserById(id);
                if (result == null)
                {
                    return NotFound(new { message = $"User with ID {id} not found." });
                }
                return Ok(new {Result= result });
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
                    return StatusCode(StatusCodes.Status200OK, new { message = "Inserted Successfully." });
                else if (res == -1)
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Email already exists." });
                else if (res == -2)
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Password does not meet the required complexity (8 characters, 1 uppercase, 1 lowercase, 1 special character, 1 number)." });
                else if (res == -3)
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Email is not correct format." });
                else
                    return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] User userDto)
        {
            try
            {
                if (string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password))
                {
                    return StatusCode(StatusCodes.Status400BadRequest,new { message = "Email and password are required." });
                }
                var user = await _userService.GetUser(userDto.Email, userDto.Password);
                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid email or password." });
                }

                var token = _userService.GenerateToken(user);
                return StatusCode(StatusCodes.Status200OK, new { Token = token, message = "Login Successful." });
            }
            catch(Exception ex)
            {
                return Unauthorized(new { message = $"An error occurred: {ex.Message}" });
            }
        }

    }
}
