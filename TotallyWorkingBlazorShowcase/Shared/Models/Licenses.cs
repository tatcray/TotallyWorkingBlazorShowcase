using System.ComponentModel.DataAnnotations;

namespace TotallyWorkingBlazorShowcase.Shared.Models
{
    public class LicensesInputModel
    {
        [Required] public string Type { get; set; }
        
        public string SelectedValue { get; set; } = "Пробный";
        public List<string> Options { get; set; } = new List<string> { "Пробный", "Профессиональный", "Корпоративная лицензия" };
        
    }
}