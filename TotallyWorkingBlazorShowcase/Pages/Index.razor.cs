using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using TotallyWorkingBlazorShowcase.Shared.Models;

namespace TotallyWorkingBlazorShowcase.Pages
{
    public class RegistrationModelBase : ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; }

        [Inject]
        private ILogger<RegisterModel> logger { get; set; }
        
        [Inject]
        private  AppDbContext _context { get; set; }
        
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        
        protected RegisterModel RegisterModel = new();

        protected async Task RegistrationUser()
        {
            User user = new User();
            user.Id = System.Guid.NewGuid().ToString();
            user.UserName = RegisterModel.UserName;
            user.Password = RegisterModel.Password;
            _context.Users.Add(user);
            var insertedRowsCount = await _context.SaveChangesAsync();
            
            if (insertedRowsCount > 0)
            {
                _navigationManager.NavigateTo("/login");
            }
        }
    }
}
