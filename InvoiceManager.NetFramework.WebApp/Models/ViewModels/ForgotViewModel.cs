using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.NetFramework.WebApp.Models.ViewModels
{
	public class ForgotViewModel
	{
		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}
}