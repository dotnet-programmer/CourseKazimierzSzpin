using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.WebApp.NetFramework.Models.ViewModels
{
	public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
