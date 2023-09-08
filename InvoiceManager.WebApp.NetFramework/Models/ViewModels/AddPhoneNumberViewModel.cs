using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.WebApp.NetFramework.Models.ViewModels
{
	public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}