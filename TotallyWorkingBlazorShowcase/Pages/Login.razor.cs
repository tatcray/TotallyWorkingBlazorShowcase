using System.Security.Cryptography;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TotallyWorkingBlazorShowcase.Shared.Models;
using Konscious.Security.Cryptography;
using TotallyWorkingBlazorShowcase.Services;

namespace TotallyWorkingBlazorShowcase.Pages
{
    public class LoginModel : ComponentBase
    {

        public LoginInputModel Input { get; set; } = new();

        [Inject] 
        private AppDbContext _context { get; set; }
        public string ErrorMessage { get; set; }

        [Inject]
        private ILocalStorageService localStorage { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        public async void UserExistsAsync()
        {
            var service = new UserService(_context);
            var UserName = Input.UserName;
            var users = _context.Users.Where(user => user.UserName.Equals(UserName));
            UserDb userDb = users.First();
            /*return users.Any();*/
            if (!users.IsNullOrEmpty())
            {
                var codeResponse = await service.UserLogin(Input);
                if (codeResponse.StatusCode == 200)
                {
                    var cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddDays(1); // Set the expiry date as per your need
                    
                    //ToDo: заменить юзер айди на что-то другое (спросить Влада)
                    await localStorage.SetItemAsStringAsync("userId", userDb.Id);
                    _navigationManager.NavigateTo("/profile");
                }
            }
        }
    }
}