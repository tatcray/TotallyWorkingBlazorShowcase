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
        public string ErrorMessage { get; set; }

        [Inject]
        private ILocalStorageService localStorage { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Inject]
        private IUserInterface _userInterface { get; set; }

        public async void UserExistsAsync()
        {

            var codeResponse = await _userInterface.UserLogin(Input);
            if (codeResponse.StatusCode == 200)
            {
                    var cookieOptions = new CookieOptions();
                    cookieOptions.Expires = DateTime.Now.AddDays(1); // Set the expiry date as per your need
                    
                    //ToDo: заменить юзер айди на что-то другое (спросить Влада)
                    await localStorage.SetItemAsStringAsync("userId", codeResponse.additionalInfo["userId"]);
                    _navigationManager.NavigateTo("/profile");
            }
        }
    }
}