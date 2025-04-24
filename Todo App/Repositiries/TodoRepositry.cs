using Microsoft.EntityFrameworkCore;
using Todo_App.Data;
using Todo_App.Data.DTOs;
using Todo_App.Interfaces;

namespace Todo_App.Repositiries
{
    public class TodoRepositry : ITodoInterface
    {
        private readonly AppDbContext _context;
        public TodoRepositry(AppDbContext context)
        {
            _context = context;
        }
        public async Task<TodoItem> CreateTodoItem(TodoDTo todo)
        {
            if (string.IsNullOrEmpty(todo.Title) || string.IsNullOrEmpty(todo.Description))
            {
                return null;
            }
            var todoItem = new TodoItem
            {
                Title = todo.Title,
                Description = todo.Description,
                IsCompleted = false,
                CreatedAt = DateTime.Now,
                UserId = todo.UserId
            };
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }
        public async Task<List<TodoItem>> GetAllTodoItems(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return null;
            }
            var todoItems = await _context.TodoItems.Where(t => t.UserId == userId).ToListAsync();
            if (todoItems == null || todoItems.Count == 0)
            {
                return null;
            }
            return todoItems;
        }
        public async Task<bool> MarkTodoItem(int TodoId, string user)
        {
            if (TodoId <= 0)
            {
                return false;
            }
            var todoItem = await _context.TodoItems.FindAsync(TodoId);
            if (todoItem == null || todoItem.UserId != user)
            {
                return false;
            }
            
            todoItem.IsCompleted = !todoItem.IsCompleted;
            _context.TodoItems.Update(todoItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
    
    
}
