using System.ComponentModel.DataAnnotations;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TotallyWorkingBlazorShowcase.Shared.Models;

namespace TotallyWorkingBlazorShowcase.Pages
{
    public class DistribModel : ComponentBase
    {
        protected DistribInputModel distribModel = new();
        protected DistribInputModel? preloadedModel = new() {Email = "почта@zeluslugi.ru", Phone = "8-499-745-0303"};

        [Inject] private AppDbContext _dbContext { get; set; }
        
        [Inject]
        private ILocalStorageService localStorage { get; set; }

        public async Task LoadDistrib()
        {
            var userId = await localStorage.GetItemAsync<string>("userId");
            if (userId != null)
            {
                preloadedModel =  await _dbContext.Distrib.FirstOrDefaultAsync(e => e.UserID == userId);
            }

            
        }

        public async Task SaveDistrib()
        {
            var userId = await localStorage.GetItemAsync<string>("userId");
            var model =  await _dbContext.Distrib.FirstOrDefaultAsync(e => e.UserID == userId);
            if (model == null)
            {
                distribModel.Id = Guid.NewGuid().ToString();
                _dbContext.Distrib.Add(distribModel);
                return;
            }

            if (!distribModel.Email.IsNullOrEmpty())
            {
                model.Email = distribModel.Email;
            }
            if (!distribModel.Phone.IsNullOrEmpty())
            {
                model.Phone = distribModel.Phone;
            }
            _dbContext.Distrib.Update(model);
            await _dbContext.SaveChangesAsync();
        }
    }
}