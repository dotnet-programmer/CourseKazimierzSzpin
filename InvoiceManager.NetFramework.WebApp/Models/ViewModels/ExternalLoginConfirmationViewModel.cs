using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.NetFramework.WebApp.Models.ViewModels
{
	public class ExternalLoginConfirmationViewModel
	{
		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}
}