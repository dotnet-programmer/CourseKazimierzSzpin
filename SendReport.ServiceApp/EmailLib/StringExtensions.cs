using System.Text.RegularExpressions;

namespace SendReport.ServiceApp.EmailLib
{
	public static class StringExtensions
	{
		public static string StripHTML(this string input)
			=> Regex.Replace(input, "<.*?>", string.Empty);
	}
}