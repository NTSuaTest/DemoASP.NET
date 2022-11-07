using Demo.DTOs;
using Demo.DTOs.User;
using Demo.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")] 
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            ErrorModel error = await _userService.Register(model);
            if (!error.IsEmpty)
                return BadRequest(error);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            return Ok();
        }
    }
}
