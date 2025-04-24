using Todo_App.Data;
using Todo_App.Data.DTOs;

namespace Todo_App.Interfaces
{
    public interface IUserInterface
    {
        Task<User> Register(RegisterDTO register);
        Task<User> Login(LoginDTO login);
        Task<bool> Logout();
    }
}
