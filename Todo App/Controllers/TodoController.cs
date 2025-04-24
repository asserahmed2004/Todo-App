using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo_App.Data.DTOs;
using Todo_App.Interfaces;

namespace Todo_App.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoInterface _todoRepositry;
        public TodoController(ITodoInterface todoRepositry)
        {
            _todoRepositry = todoRepositry;
        }
        [HttpPost("Create a new Todo")]
        
        public async Task<IActionResult> Create([FromBody]TodoDTo todo)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return Unauthorized("User is not authenticated");
            }
            var user = User.Claims.ToList()[0].Value;
            todo.UserId = user;
            var result = await _todoRepositry.CreateTodoItem(todo);
            if (result == null)
            {
                return BadRequest("Invalid Todo item");
            }
            return Ok(result);
        }
        [HttpGet("Get All Todos")]
        public async Task<IActionResult> GetAll(string userId)
        {
            if(User.Identity.IsAuthenticated == false)
            {
                return Unauthorized("User is not authenticated");
            }
            var user = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;


            var result = await _todoRepositry.GetAllTodoItems(user);
            if (result == null)
            {
                return BadRequest("No Todo items found");
            }
            return Ok(result);
        }
        [HttpPut("Mark Done")]
        public async Task<IActionResult> Mark(int TodoId)
        {
            if(User.Identity.IsAuthenticated == false)
            {
                return Unauthorized("User is not authenticated");
            }
            var user = User.FindFirst("UserId")?.Value;
            var result = await _todoRepositry.MarkTodoItem(TodoId,user);
            if (!result)
            {
                return BadRequest("Invalid Todo item");
            }
            return Ok(result);
        }
    }
}
