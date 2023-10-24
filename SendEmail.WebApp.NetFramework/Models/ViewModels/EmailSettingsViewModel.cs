using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendEmail.WebApp.NetFramework.Models.Domains;

namespace SendEmail.WebApp.NetFramework.Models.ViewModels
{
	public class EmailSettingsViewModel
	{
        //public EmailSettings EmailSettings { get; set; }
        public List<EmailSettings> EmailSettings { get; set; }
    }
}