using System.ComponentModel.DataAnnotations;

namespace TrackingManagment.ViewModel
{
    public class RegisterView
    {
        
        public string? UserName { get; set; }  
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}
