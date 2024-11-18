using Microsoft.AspNetCore.Mvc;
using SimpleFly_BookingSystem.Interfaces;
using SimpleFly_BookingSystem.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace SimpleFly_BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            var user = _userService.Register(model.Username, model.Password, model.Role, model.Email);
            if (user == null)
            {
                return BadRequest(new { Message = "User registration failed: User already exists." });
            }
            return Ok(user); 
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var token = _userService.Authenticate(model.Username, model.Password);
            if (token == null)
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }
            return Ok(new { Token = token });
        }
    }
}
