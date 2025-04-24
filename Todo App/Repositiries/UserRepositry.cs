using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Todo_App.Data;
using Todo_App.Data.DTOs;
using Todo_App.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace Todo_App.Repositiries
{
    public class UserRepositry : IUserInterface
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepositry(AppDbContext context,UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<User> Login(LoginDTO login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return null;
            }
            var user =await _userManager.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
            
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            var claimType = "UserId";
            
            claimsIdentity.AddClaim(new Claim(claimType,user.Id ));
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await _httpContextAccessor.HttpContext.SignInAsync(claimsPrincipal);




            if (user == null)
            {
                return null;
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
            if (result.Succeeded)
            {
                return user;
            }
            else
            {
                return null;
            }

        }
        public async Task<User> Register(RegisterDTO register)
        {
            if (register.Email.Contains("@") == false)
            {
                return null;
            }
            var user = new User
            {
                UserName = register.UserName,
                Email = register.Email
                
            };
            var result =await _userManager.CreateAsync(user, register.Password);



            if (result.Succeeded)
            {
                var login = new LoginDTO
                {
                    Email = register.Email,
                    Password = register.Password
                };
                await Login(login);
                return user;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return false;
            }
             
            
        }
    }
    
    
}
