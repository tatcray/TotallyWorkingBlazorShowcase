using System.ComponentModel.DataAnnotations;

namespace TotallyWorkingBlazorShowcase.Shared.Models
{
    public class LoginInputModel
    {
        [Required]
        public string UserName { get; set; }

        [Required] 
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}

