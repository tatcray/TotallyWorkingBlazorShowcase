using Microsoft.AspNetCore.Components;

namespace TotallyWorkingBlazorShowcase.Pages
{
    public class Modal : ComponentBase
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        public bool Show { get; private set; }

        public void Open()
        {
            Show = true;
            StateHasChanged();
        }

        public void Close()
        {
            Show = false;
            StateHasChanged();
        }
    }   
}