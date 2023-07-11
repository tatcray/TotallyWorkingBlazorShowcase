using System.Security.Cryptography;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using TotallyWorkingBlazorShowcase.Shared.Models;
using Konscious.Security.Cryptography;

namespace TotallyWorkingBlazorShowcase.Pages
{
    public class RegistrationModelBase : ComponentBase
    {
        private const int saltSize = 16;
        private const int hashSize = 16;
        private const int iterations = 10000;
        private const string secretPepper = "Secret 16 Byte pepper.";
        
        
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
            
            byte[] salt = new byte[saltSize];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with a random value.
                rngCsp.GetBytes(salt);
            }
            User user = new User();
            user.Id = Guid.NewGuid().ToString();
            user.UserName = RegisterModel.UserName;
            user.Password = RegisterModel.Password;
            user.Salt = Convert.ToBase64String(salt);
            var password = System.Text.Encoding.UTF8.GetBytes(user.Password);
            var argon2 = new Argon2d(password);
            
            argon2.DegreeOfParallelism = 16;
            argon2.MemorySize = 8192;
            argon2.Iterations = 40;

            argon2.Salt = salt;
            var hash = argon2.GetBytes(128);

            user.Password = Convert.ToBase64String(hash);
            
            _context.Users.Add(user);
            var insertedRowsCount = await _context.SaveChangesAsync();
            
            if (insertedRowsCount > 0)
            {
                _navigationManager.NavigateTo("/login");
            }
        }
    }
}
