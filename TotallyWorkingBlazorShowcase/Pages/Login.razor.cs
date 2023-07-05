using System.Security.Cryptography;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TotallyWorkingBlazorShowcase.Shared.Models;

namespace TotallyWorkingBlazorShowcase.Pages
{
    public class LoginModel : ComponentBase
    {
        private const int saltSize = 16;
        private const int hashSize = 16;
        private const int iterations = 10000;
        private const string secretPepper = "Secret 16 Byte pepper.";
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
            var UserName = Input.UserName;

            var users = _context.Users.Where(user => user.UserName.Equals(UserName));
            /*return users.Any();*/
            if (!users.IsNullOrEmpty())
            {
                var usersalt = users.First().Salt;
                byte[] salt = Convert.FromBase64String(usersalt);
                Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(Input.Password + secretPepper, salt, iterations);
                byte[] hash = pbkdf2.GetBytes(hashSize);
                string hashPassword = Convert.ToBase64String(hash);

                User user = users.First();
                if (user.Password.Equals(hashPassword))
                {
                    var cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddDays(1); // Set the expiry date as per your need
                    
                    //ToDo: заменить юзер айди на что-то другое (спросить Влада)
                    await localStorage.SetItemAsStringAsync("userId", user.Id);
                    _navigationManager.NavigateTo("/profile");
                }
            }
        }
    }
}