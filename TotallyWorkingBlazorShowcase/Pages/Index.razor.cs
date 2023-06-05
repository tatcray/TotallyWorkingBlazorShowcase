/*using Microsoft.AspNetCore.Components;

namespace TotallyWorkingBlazorShowcase.Pages
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class RegisterModel : ComponentBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private RegisterModel _registerModel = new RegisterModel();

        [BindProperty] public RegisterModel Input { get; set; }
     
        [Required] [EmailAddress] public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
            
        private List<string> errors;

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            errors = new List<string>();

            var user = new User { UserName = Input.UserName };
            var result = await _userManager.CreateAsync(user, Input.Password);    
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                // Additional code for logging in the user or sending an email confirmation...
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
            }
        }
    }
}*/

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

        protected RegisterModel RegisterModel = new();


        [Inject]
        private  AppDbContext _context { get; set; }

        protected async Task RegistrationUser()
        {
            User user = new User();
            user.Id = System.Guid.NewGuid().ToString();
            user.UserName = RegisterModel.UserName;
            user.Password = RegisterModel.Password;
            _context.Users.Add(user);
            _context.SaveChanges();
            //await Http.PostAsJsonAsync("api/Register", RegisterModel);
        }
    }
}
