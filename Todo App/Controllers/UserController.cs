using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo_App.Data.DTOs;
using Todo_App.Interfaces;

namespace Todo_App.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userRepositry;
        public UserController(IUserInterface userRepositry)
        {
            _userRepositry = userRepositry;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO register)
        {
            var result = await _userRepositry.Register(register);
            if (result == null)
            {
                return BadRequest("Invalid registration");
            }
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var result = await _userRepositry.Login(login);
            if (result == null)
            {
                return BadRequest("Invalid login");
            }
            
            return Ok(result);
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _userRepositry.Logout();
            if (result == false)
            {
                return BadRequest("Invalid logout");
            }
            else
            {
                return Ok("Logout successful");
            }
            
        }
    }
}
