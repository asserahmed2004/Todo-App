using Todo_App.Data;
using Todo_App.Data.DTOs;

namespace Todo_App.Interfaces
{
    public interface ITodoInterface
    {
        Task<TodoItem> CreateTodoItem(TodoDTo todo);
        Task<List<TodoItem>> GetAllTodoItems(string userId);
        Task<bool> MarkTodoItem(int TodoId,string user);
        
      
        
    }
}
