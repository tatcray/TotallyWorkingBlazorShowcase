using System.ComponentModel.DataAnnotations;

namespace TotallyWorkingBlazorShowcase.Shared.Models
{
    public class ProfileInputModel
    {
        [Required] public string Name { get; set; }

        [Required] public string APIMonitoring { get; set; }

        [Required] public string APIServices { get; set; }
    }
}