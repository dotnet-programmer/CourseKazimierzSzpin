using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.NetFramework.WebApp.Models.ViewModels
{
	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}
}