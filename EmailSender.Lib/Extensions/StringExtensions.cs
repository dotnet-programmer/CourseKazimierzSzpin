using System.Text.RegularExpressions;

namespace EmailSender.Lib.Extensions;

public static class StringExtensions
{
	public static string StripHTML(this string input) => Regex.Replace(input, "<.*?>", string.Empty);
}