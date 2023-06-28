using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using TotallyWorkingBlazorShowcase.Shared.Models;

namespace TotallyWorkingBlazorShowcase.Pages
{
    public class LicensesModel : ComponentBase
    {
        protected LicensesInputModel licensesModel = new();

        public bool isModalVisible;

        public void OpenModal()
        {
            isModalVisible = true;
        }

        public void ModalClosed()
        {
            isModalVisible = false;
        }
    }
}
