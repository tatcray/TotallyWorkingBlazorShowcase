using System.Security.Cryptography;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TotallyWorkingBlazorShowcase.Shared.Models;
using Konscious.Security.Cryptography;

namespace TotallyWorkingBlazorShowcase.Pages
{
    public class LoginModel : ComponentBase
    {
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
                
                var password = System.Text.Encoding.UTF8.GetBytes(Input.Password);
                var argon2 = new Argon2d(password);
            
                argon2.DegreeOfParallelism = 16;
                argon2.MemorySize = 8192;
                argon2.Iterations = 40;

                argon2.Salt = salt;
                var hash = argon2.GetBytes(128);

                
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