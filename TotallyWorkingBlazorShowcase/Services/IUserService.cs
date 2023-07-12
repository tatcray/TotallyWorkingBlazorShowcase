using TotallyWorkingBlazorShowcase.Shared.Models;

namespace TotallyWorkingBlazorShowcase.Services
{

    public interface IUserInterface
    {
        Task<ServisResponse> SaveUser(RegisterModel user);
        Task<ServisResponse> UserLogin(LoginInputModel inputModel);
    }
}