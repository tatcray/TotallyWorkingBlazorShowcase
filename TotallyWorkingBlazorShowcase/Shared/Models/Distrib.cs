using System.ComponentModel.DataAnnotations;

namespace TotallyWorkingBlazorShowcase.Shared.Models
{
    public class DistribInputModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
        public string Phone { get; set; }
        
        public string Description { get; set; }
        
        public string Password { get; set; }

        public string Link { get; set; }
        
        public string Email { get; set; }

        public string UserID { get; set; }

        /* TODO
        [Required] public string Photo { get; set; }
        */

        
        /*[Required] public string APIMonitoring { get; set; }

        [Required] public string APIServices { get; set; }*/
    }
}