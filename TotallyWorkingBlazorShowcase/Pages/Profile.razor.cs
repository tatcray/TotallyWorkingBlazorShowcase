using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using TotallyWorkingBlazorShowcase.Shared.Models;

namespace TotallyWorkingBlazorShowcase.Pages
{
    public class ProfileModel : ComponentBase
    {
        protected ProfileInputModel profileModel = new();
    }
}