using System.Collections.Generic;

namespace SendEmail.WebApp.NetFramework.Models.ViewModels
{
	public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}