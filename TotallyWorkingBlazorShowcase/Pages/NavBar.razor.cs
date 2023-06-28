using Microsoft.AspNetCore.Components;

namespace TotallyWorkingBlazorShowcase.Pages
{
    public partial class NavBarModel : ComponentBase
    {
        public bool isNavVisible = true;
        public string navBarClass = "nav-bar";

        public void ToggleNavBar()
        {
            isNavVisible = !isNavVisible;
            navBarClass = isNavVisible ? "nav-bar" : "nav-bar-hidden";
        }
    }
}