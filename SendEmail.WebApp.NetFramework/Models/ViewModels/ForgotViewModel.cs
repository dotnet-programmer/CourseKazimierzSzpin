using System.ComponentModel.DataAnnotations;

namespace SendEmail.WebApp.NetFramework.Models.ViewModels
{
	public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
