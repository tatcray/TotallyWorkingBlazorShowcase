using System.Security.Cryptography;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using TotallyWorkingBlazorShowcase.Shared.Models;
using Konscious.Security.Cryptography;
using TotallyWorkingBlazorShowcase.Services;

namespace TotallyWorkingBlazorShowcase.Pages
{
    public class RegistrationModelBase : ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; }

        [Inject]
        private ILogger<RegisterModel> logger { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        protected RegisterModel registerModel = new();

        [Inject] 
        private IUserInterface _userInterface { get; set; }

        protected async Task RegistrationUser()
        {
            var codeResponse = await _userInterface.SaveUser(registerModel);
            if (codeResponse.StatusCode == 200)
            {
                _navigationManager.NavigateTo("/login");
            }
        }
    }
}
