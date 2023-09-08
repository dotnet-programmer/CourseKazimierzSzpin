using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.WebApp.NetFramework.Models.ViewModels
{
	public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
