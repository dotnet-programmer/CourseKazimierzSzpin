using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

// INFO - własny Action Result

namespace InvoiceManager.NetFramework.WebApp.ActionResults
{
	public class MyCsvResult : ActionResult
	{
		public Encoding ContentEncoding { get; set; }
		public string Content { get; set; }
		public string Name { get; set; }

		public MyCsvResult(string content) => this.Content = content;

		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			HttpResponseBase response = context.HttpContext.Response;

			response.ContentType = "text/csv";

			if (ContentEncoding != null)
			{
				response.ContentEncoding = ContentEncoding;
			}

			var fileName = "file.csv";

			if (!string.IsNullOrEmpty(Name))
			{
				fileName = Name.Contains('.') ? Name : Name + ".csv";
			}

			response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));

			if (Content != null)
			{
				response.Write(Content);
			}
		}
	}
}