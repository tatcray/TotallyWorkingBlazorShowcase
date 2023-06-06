using Microsoft.AspNetCore.Components;
using TotallyWorkingBlazorShowcase.Shared.Models;

namespace TotallyWorkingBlazorShowcase.Pages
{
    public class LoginModel : ComponentBase
    {
        public LoginInputModel Input { get; set; } = new();

        [Inject] 
        private AppDbContext _context { get; set; }
        public string ErrorMessage { get; set; }

        [Inject] private NavigationManager _navigationManager { get; set; }

        public async void UserExistsAsync()
        {
            var UserName = Input.UserName;
            var Password = Input.Password;

            var users = _context.Users.Where(user => user.UserName.Equals(UserName) && user.Password.Equals(Password));
            /*return users.Any();*/
            if (users.Any())
            {
                _navigationManager.NavigateTo("/home");
            }
        }
    }
}